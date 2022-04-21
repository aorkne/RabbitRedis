using MassTransit;
using Producer;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri("rabbitmq://rabbitmq"), h =>
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
