namespace Domain.RabbitMQ;

public record Ticket(Guid Id, string Message, TicketType Type, DateTime CreatedAt);

public enum TicketType
{
    Default = 1,
    Plus = 2,
    Premium = 3,
    Vip = 4
}