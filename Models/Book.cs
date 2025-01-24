using System.ComponentModel.DataAnnotations;
using LMSAPI.Utilities;

namespace LMSAPI.Models;

//Class which define the Book object
public class Book
{
     
     [Key]
     public int BookId { get; set; }

     [Required]
     public string? Title { get; set; }

     [Required]
     [RegularExpression(ModelConstants.AuthorPattern, ErrorMessage = ModelConstants.AuthorErrorMessage)]
     public string? Author { get; set; }

     [Required]
     [RegularExpression(ModelConstants.CopiesAvailablePatter, ErrorMessage = ModelConstants.CopiesAvailableErrorMessage)]
     public int CopiesAvailable { get; set; }
}