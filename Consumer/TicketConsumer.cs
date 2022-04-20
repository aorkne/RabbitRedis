using Domain.RabbitMQ;
using Newtonsoft.Json;
using MassTransit;

namespace Consumer;
public class TicketConsumer : IConsumer<Ticket>
{
    public async Task Consume(ConsumeContext<Ticket> context)
    {
        var data = context.Message;

        Console.WriteLine($"Received: {JsonConvert.SerializeObject(data)}");
        //Validate the Ticket Data
        //Store to Database
        //Notify the user via Email / SMS
    }
}