using AutoMapper;
using Consumer;
using Consumer.Data;
using Domain.RabbitMQ;
using MassTransit;
using StackExchange.Redis;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var provider = services.BuildServiceProvider();
        var configuration = provider.GetService<IConfiguration>();
        
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new AutoMapperProfile());
        });

        var mapper = config.CreateMapper();
        
        services.AddSingleton(mapper);
        
        services.AddSingleton<IConnectionMultiplexer>(opt => 
            ConnectionMultiplexer.Connect(configuration.GetConnectionString("DockerRedisConnection")));
        
        services.AddScoped<IRedisRepo, RedisRepo>();
        
        services.AddMassTransit(x =>
        {
            x.AddConsumer<TicketConsumer>();
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    RabbitMqHelper helper = new();
                    
                    config.Host(new Uri(helper.ConnectionUrl), h =>
                    {
                        h.Username("admin");
                        h.Password("123456");
                    });
                    config.ReceiveEndpoint("ticketQueue", ep =>
                    {
                        ep.PrefetchCount = 16;
                        ep.UseMessageRetry(r => r.Interval(2, 100));
                        ep.ConfigureConsumer<TicketConsumer>(provider);
                    });
                }));
        });
    })
    .Build();

await host.RunAsync();
