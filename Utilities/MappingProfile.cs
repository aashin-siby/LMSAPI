using AutoMapper;
using LMSAPI.DTO;
using LMSAPI.Models;

namespace LMSAPI.Utilities;
public class MappingProfile : Profile
{
     public MappingProfile()
     {
          CreateMap<User, UserDto>().ReverseMap();


     }

}