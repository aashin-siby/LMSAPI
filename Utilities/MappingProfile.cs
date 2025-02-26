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
            .ForMember(borrowDetailsDto => borrowDetailsDto.Title, option => option.MapFrom(borrowDetails => borrowDetails.Book.Title))
            .ForMember(borrowDetailsDto => borrowDetailsDto.Payment, option => option.MapFrom(borrowDetails => borrowDetails.Payment))
            .ForMember(borrowDetailsDto => borrowDetailsDto.BorrowId, option => option.MapFrom(borrowDetails => borrowDetails.BorrowId))
            .ForMember(borrowDetailsDto => borrowDetailsDto.BorrowDate, option => option.MapFrom(borrowDetails =>
                borrowDetails.BorrowDate == DateTime.MinValue ? DateTime.UtcNow : borrowDetails.BorrowDate))
            .ForMember(borrowDetailsDto => borrowDetailsDto.ReturnDate, opt => opt.MapFrom(borrowDetails =>
                borrowDetails.ReturnDate.HasValue && borrowDetails.ReturnDate.Value == DateTime.MinValue
                    ? (DateTime?)null
                    : borrowDetails.ReturnDate));
    }
}
