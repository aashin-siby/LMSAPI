using System.ComponentModel.DataAnnotations;

namespace LMSAPI.DTO;
//Dto to view the bills of the particular user
public class BorrowDetailsDto
{
     public int BookId { get; set; }

     [Required]
     public string? Title { get; set; }
     public decimal Payment { get; set; }
}