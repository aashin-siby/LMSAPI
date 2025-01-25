using System.ComponentModel.DataAnnotations;

using LMSAPI.Utilities;

namespace LMSAPI.DTO;

public class UserLogin
{
     [Required]
     [StringLength(50, MinimumLength = 3)]
     [RegularExpression(ModelConstants.UsernamePatter, ErrorMessage = ModelConstants.UsernameErrorMessage)]
     public string Username { get; set; } = string.Empty;

     [Required]
     [StringLength(100, MinimumLength = 6)]
     [RegularExpression(ModelConstants.PasswordPattern, ErrorMessage = ModelConstants.PasswordErrorMessage)]
     public string Password { get; set; } = string.Empty;
}