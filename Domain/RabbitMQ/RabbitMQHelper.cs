namespace Domain.RabbitMQ;

public class RabbitMqHelper
{
    bool? _isRunningInContainer;

    bool IsRunningInContainer => _isRunningInContainer ??=
        bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) &&
        inContainer;
    
    public string ConnectionUrl => IsRunningInContainer ?
        "rabbitmq://rabbitmq" :
        "localhost://rabbitmq";
    
    public string TicketQueueName => "ticketQueue";
}