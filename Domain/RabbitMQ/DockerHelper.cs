namespace Domain.RabbitMQ;

public class DockerHelper
{
    bool? _isRunningInContainer;

    bool IsRunningInContainer => _isRunningInContainer ??=
        bool.TryParse(Environment.GetEnvironmentVariable("DOTNET_RUNNING_IN_CONTAINER"), out var inContainer) &&
        inContainer;
    
    public string RabbitMqConnectionUrl => IsRunningInContainer ?
        "rabbitmq://rabbitmq" :
        "rabbitmq://localhost";
    
    public string RabbitMqTicketQueueName => "ticketQueue";
}