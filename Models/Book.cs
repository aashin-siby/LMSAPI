using System.ComponentModel.DataAnnotations;
using LMSAPI.Utilities;

namespace LMSAPI.Models;

//Model which define the Book entity
public class Book
{

     [Key]
     public int BookId { get; set; }

     [Required]
     public string? Title { get; set; }

     public string? ImageUrl { get; set; }

     public string? BookDescription { get; set; }

     [Required]
     [RegularExpression(ModelConstants.AuthorPattern, ErrorMessage = ModelConstants.AuthorErrorMessage)]
     public string? Author { get; set; }

     [Required]
     [RegularExpression(ModelConstants.CopiesAvailablePattern, ErrorMessage = ModelConstants.CopiesAvailableErrorMessage)]
     public int CopiesAvailable { get; set; }

     public ICollection<BorrowDetails>? BorrowDetails { get; set; }

}