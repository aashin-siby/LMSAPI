using System.ComponentModel.DataAnnotations;

namespace LMSAPI.DTO;

//Dto for iterating Rental Details for User and Admin
public class RentalDetailsDto
{
    public int BorrowId { get; set; }
    public int BookId { get; set; }
    public string? Title { get; set; }
    public int UserId { get; set; }
    public string? Username { get; set; }
    public decimal Payment { get; set; }

    [DataType(DataType.Date)]
    public DateTime? ReturnDate { get; set; }
}

