using Domain.RabbitMQ;

namespace Consumer.Data;
public class RedisTicket
{
    public Guid Id{get; set; }
    public string Message { get; set; }
    public TicketType Type { get; set; }
    public DateTime CreatedAt { get; set; }
    
    public string RedisId => $"tickets-{Id:N}";
}