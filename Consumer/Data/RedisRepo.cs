using System.Text.Json;
using StackExchange.Redis;

namespace Consumer.Data;

public class RedisRepo:IRedisRepo
{
    public async Task AddTicket(RedisTicket ticket)
    {
        if (ticket == null)
        {
            throw new ArgumentOutOfRangeException(nameof(ticket));
        }

        var db = _redis.GetDatabase();

        var serialPlat = JsonSerializer.Serialize(ticket);

        await db.StringSetAsync(ticket.RedisId, serialPlat);
    }

    public async Task<RedisTicket?> GetTicket(string ticketId)
    {
        var db = _redis.GetDatabase();

        var ticket = await db.StringGetAsync(ticketId);

        if (!string.IsNullOrEmpty(ticket))
        {
            return JsonSerializer.Deserialize<RedisTicket>(ticket);
        }

        return null;

    }

    private readonly IConnectionMultiplexer _redis;

    public RedisRepo(IConnectionMultiplexer redis)
    {
        _redis = redis;
    }
}