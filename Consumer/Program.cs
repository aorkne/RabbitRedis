using Consumer;
using Domain.RabbitMQ;
using MassTransit;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<TicketConsumer>();
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
                {
                    config.Host(new Uri("rabbitmq://localhost"), h =>
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
        //services.AddMassTransitHostedService();

        //services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
