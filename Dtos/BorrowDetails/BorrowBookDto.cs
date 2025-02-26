using System.ComponentModel.DataAnnotations;

namespace LMSAPI.DTO;

//Borrow Dto to map when borrowing a  book 
public class BorrowBookDto
{
     [Required]
     public int UserId { get; set; }
     public string? Title { get; set; }

     [Required]
     public int BookId { get; set; }

     [Required]
     [DataType(DataType.Date)]
     public DateTime BorrowDate { get; set; }

     public decimal Payment = 100;
}
