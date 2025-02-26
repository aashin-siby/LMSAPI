using System.ComponentModel.DataAnnotations;
using LMSAPI.Utilities;
namespace LMSAPI.Models;

//Model which defines the User Entity
public class User
{

     [Key]
     public int UserId { get; set; }

     [Required]
     [RegularExpression(ModelConstants.UsernamePattern, ErrorMessage = ModelConstants.UsernameErrorMessage)]
     public string Username { get; set; } = string.Empty;

     [Required]
     [RegularExpression(ModelConstants.PasswordPattern, ErrorMessage = ModelConstants.PasswordErrorMessage)]
     public string Password { get; set; } = string.Empty;

     [Required]
     [RegularExpression(ModelConstants.RolePattern, ErrorMessage = ModelConstants.RoleErrorMessage)]
     public string? Role { get; set; }
}
