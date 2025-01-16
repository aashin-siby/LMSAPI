using System.ComponentModel.DataAnnotations;

namespace LMSAPI.Models;

//Class which define the Book object
public class Book
{
     //Defines the primary key
     [Key]
     public int BookId { get; set; }

     [Required]
     public string? Title { get; set; }

     [Required]
     public string? Author { get; set; }
     
     [Required]
     public int CopiesAvailable { get; set; }
}