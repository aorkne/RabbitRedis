using Domain.RabbitMQ;
using MassTransit;

namespace Producer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBus _bus;
    readonly DockerHelper _rabbitMqHelper;

    public Worker(ILogger<Worker> logger, IBus bus, DockerHelper rabbitMqHelper)
    {
        _logger = logger;
        _bus = bus;
        _rabbitMqHelper = rabbitMqHelper;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            Ticket ticket = new (Guid.NewGuid(), "Hello World", TicketType.Default, DateTime.Now);

            Uri uri = new Uri($"{_rabbitMqHelper.RabbitMqConnectionUrl}/{_rabbitMqHelper.RabbitMqTicketQueueName}");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(ticket);

            await Task.Delay(5000, stoppingToken);
        }
    }
}
