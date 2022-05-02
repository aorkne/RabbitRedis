using AutoMapper;
using Consumer.Data;
using Domain.RabbitMQ;

namespace Consumer;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Ticket, RedisTicket>(); //reverse so the both direction
    }
}