using System.ComponentModel.DataAnnotations;

namespace LMSAPI.DTO;
//Dto when returning the book
public class ReturnBookDto
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int BookId { get; set; }

    [Required]
    public int BorrowId { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime ReturnDate { get; set; }
}
