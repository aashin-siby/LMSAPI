using System.ComponentModel.DataAnnotations;

namespace LMSAPI.DTO;

//Dto to view the bills of the particular user
public class BorrowDetailsDto
{
     public int BookId { get; set; }
     public int BorrowId { get; set; }
     public string? Title { get; set; }
     public decimal Payment { get; set; }
     [DataType(DataType.Date)]
     public DateTime BorrowDate { get; set; }
     [DataType(DataType.Date)]
     public DateTime? ReturnDate { get; set; }
}