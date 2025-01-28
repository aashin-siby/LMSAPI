using System.ComponentModel.DataAnnotations;

namespace LMSAPI.DTO;

//Borrow Dto to map when borrowin a  book 
public class BorrowBookDto
{
     public int UserId { get; set; }

     [Required]
     public int BookId { get; set; }

     [Required]
     [DataType(DataType.Date)]
     public DateTime BorrowDate { get; set; }
}
