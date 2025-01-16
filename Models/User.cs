using System.ComponentModel.DataAnnotations;

namespace LMSAPI.Models;

//Class which defines the User
public class User
{
     [Key]
     public int UserId { get; set; }

     [Required]
     [StringLength(50, MinimumLength = 3)]
     public string Username { get; set; } = string.Empty;

     [Required]
     [StringLength(100, MinimumLength = 6)]
     public string Password { get; set; } = string.Empty;
     
     [Required]
     public string Role { get; set; } = string.Empty;
}
