namespace RabbitRedis.Domain;

public class Ticket
{
    public Guid Id { get; set; }
    public string Message { get; set; }
    public TicketType Type { get; set; }
    public DateTime CreatedAt { get; set; }
}

public enum TicketType
{
    Default = 1,
    Plus = 2,
    Premium = 3,
    Vip = 4
}