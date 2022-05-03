using Domain.RabbitMQ;
using MassTransit;
using Producer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        DockerHelper rabbitMqHelper = new();
        services.AddSingleton(rabbitMqHelper);
        
        services.AddMassTransit(x =>
        {
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri(rabbitMqHelper.RabbitMqConnectionUrl), h =>
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
