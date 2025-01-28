using System.ComponentModel.DataAnnotations;

namespace LMSAPI.DTO;
public class BorrowDetailsDto
{
     public int BookId { get; set; }

     [Required]
     public string? Title { get; set; }
     public decimal Payment { get; set; }
}