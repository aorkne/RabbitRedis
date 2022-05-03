using Domain.RabbitMQ;
using MassTransit;
using Producer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        RabbitMqHelper rabbitMqHelper = new();
        services.AddSingleton(rabbitMqHelper);
        
        services.AddMassTransit(x =>
        {
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(rabbitMqHelper.ConnectionUrl), h =>
                    {
                        h.Username("admin");
                        h.Password("123456");
                    });
                }));
        });
        
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
