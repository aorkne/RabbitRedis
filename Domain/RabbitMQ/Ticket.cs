namespace Domain.RabbitMQ;

public class Ticket
{
    public Ticket(Guid Id, string Message, TicketType Type, DateTime CreatedAt)
    {
        this.Id = Id;
        this.Message = Message;
        this.Type = Type;
        this.CreatedAt = CreatedAt;
    }

    public Guid Id { get; init; }
    public string Message { get; init; }
    public TicketType Type { get; init; }
    public DateTime CreatedAt { get; init; }

    public void Deconstruct(out Guid Id, out string Message, out TicketType Type, out DateTime CreatedAt)
    {
        Id = this.Id;
        Message = this.Message;
        Type = this.Type;
        CreatedAt = this.CreatedAt;
    }
}

public enum TicketType
{
    Default = 1,
    Plus = 2,
    Premium = 3,
    Vip = 4
}