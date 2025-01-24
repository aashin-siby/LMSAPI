using System;
using System.ComponentModel.DataAnnotations;

namespace LMSAPI.DTO
{
     public class BorrowBookDto
     {
          [Required]
          public int UserId { get; set; }

          [Required]
          public int BookId { get; set; }

          [Required]
          public DateTime BorrowDate { get; set; }
     }
}