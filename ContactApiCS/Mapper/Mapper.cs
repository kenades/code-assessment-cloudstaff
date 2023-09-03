using AutoMapper;
using ContactApiCS.Models;

namespace ContactApiCS.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<ContactCreationDto, Contact>();
            CreateMap<ContactUpdateDto, Contact>();
        }
    }
}
