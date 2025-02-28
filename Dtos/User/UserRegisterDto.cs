using System.ComponentModel.DataAnnotations;
using LMSAPI.Utilities;

namespace LMSAPI.DTO;

//Dto to map when user register with User model
public class UserRegisterDto
{
    
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