using System.ComponentModel.DataAnnotations;
using LMSAPI.Enums;
using LMSAPI.Utilities;
namespace LMSAPI.Models;

//Class which defines the User
public class User
{

     [Key]
     public int UserId { get; set; }

     [Required]
     [StringLength(50, MinimumLength = 3)]
     [RegularExpression(ModelConstants.UsernamePatter, ErrorMessage = ModelConstants.UsernameErrorMessage)]
     public string Username { get; set; } = string.Empty;

     [Required]
     [StringLength(100, MinimumLength = 6)]
     [RegularExpression(ModelConstants.PasswordPattern, ErrorMessage = ModelConstants.PasswordErrorMessage)]
     public string Password { get; set; } = string.Empty;

     [Required]
     [RegularExpression(ModelConstants.RolePattern, ErrorMessage = ModelConstants.RoleErrorMessage)]
     public UserRole Role { get; set; }
}
