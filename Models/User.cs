using System.ComponentModel.DataAnnotations;
namespace LMSAPI.Models;

//Model which defines the User Entity
public class User
{

     [Key]
     public int UserId { get; set; }

     [Required]
     public string Username { get; set; } = string.Empty;

     [Required]
     public string Password { get; set; } = string.Empty;

     [Required]
     public string? Role { get; set; }
}
