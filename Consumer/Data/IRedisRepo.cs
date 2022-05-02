namespace Consumer.Data;

public interface IRedisRepo
{
    Task AddTicket(RedisTicket ticket);
    Task<RedisTicket> GetTicket(string ticketId);
}