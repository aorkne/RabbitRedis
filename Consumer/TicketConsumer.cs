using AutoMapper;
using Consumer.Data;
using Newtonsoft.Json;
using MassTransit;
using Ticket = Domain.RabbitMQ.Ticket;

namespace Consumer;
public class TicketConsumer : IConsumer<Ticket>
{
    IRedisRepo _redisRepo;
    private readonly IMapper _mapper;

    public TicketConsumer(IRedisRepo redisRepo, IMapper mapper)
    {
        _redisRepo = redisRepo;
        _mapper = mapper;
    }

    public async Task Consume(ConsumeContext<Ticket> context)
    {
        var data = context.Message;

        Console.WriteLine($"Received: {JsonConvert.SerializeObject(data)}");
        
        var ticket = _mapper.Map<Ticket, RedisTicket>(data);
        
        await _redisRepo.AddTicket(ticket);
        
        var storedTicket = await _redisRepo.GetTicket(ticket.RedisId);
        
        Console.WriteLine($"Stored ticket: {JsonConvert.SerializeObject(storedTicket)}");
        
        //Validate the Ticket Data
        //Store to Database
        //Notify the user via Email / SMS
    }
}