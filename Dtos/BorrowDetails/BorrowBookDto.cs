using System.ComponentModel.DataAnnotations;

namespace LMSAPI.DTO
{
     public class BorrowBookDto
     {
          public int UserId { get; set; }

          [Required]
          public int BookId { get; set; }

          [Required]
          [DataType(DataType.Date)]
          public DateTime BorrowDate { get; set; }
     }
}