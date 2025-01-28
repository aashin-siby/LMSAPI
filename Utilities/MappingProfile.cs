using AutoMapper;
using LMSAPI.DTO;
using LMSAPI.Models;

namespace LMSAPI.Utilities;
//Mapping the DTOs with the models
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserRegister>().ReverseMap();
        CreateMap<User, UserLogin>().ReverseMap();

        // Book mappings
        CreateMap<Book, BookDto>().ReverseMap();

        // Borrow details mappings
        CreateMap<BorrowDetails, BorrowBookDto>().ReverseMap();
        CreateMap<BorrowDetails, ReturnBookDto>().ReverseMap();
        CreateMap<BorrowDetails, BorrowDetailsDto>()
            .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.Book.Title))
            .ForMember(dest => dest.Payment, opt => opt.MapFrom(src => src.Payment));
    }
}
