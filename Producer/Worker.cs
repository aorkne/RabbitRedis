using Domain.RabbitMQ;
using MassTransit;

namespace Producer;

public class Worker : BackgroundService
{
    private readonly ILogger<Worker> _logger;
    private readonly IBus _bus;

    public Worker(ILogger<Worker> logger, IBus bus)
    {
        _logger = logger;
        _bus = bus;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            Ticket ticket = new Ticket(){
                Id = Guid.NewGuid(),
                Message = "Hello World",
                Type = TicketType.Default,
                CreatedAt = DateTime.Now
            };

            Uri uri = new Uri("rabbitmq://rabbitmq/ticketQueue");
            var endPoint = await _bus.GetSendEndpoint(uri);
            await endPoint.Send(ticket);

            await Task.Delay(1000, stoppingToken);
        }
    }
}
