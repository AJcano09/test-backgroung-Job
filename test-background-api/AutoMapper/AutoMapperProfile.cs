using AutoMapper;
using test_background_api.dbContext.Entities;
using test_background_api.Models;

namespace test_background_api.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
         CreateMap<Message, MessageDto>();
         CreateMap<MessageDto, Message>();
    }
   
}