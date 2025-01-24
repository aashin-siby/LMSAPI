using System.ComponentModel.DataAnnotations;
using LMSAPI.Enums;
using LMSAPI.Utilities;

namespace LMSAPI.DTO;

public class UserDto
{
    public int UserId { get; set; }

    [RegularExpression(ModelConstants.UsernamePatter, ErrorMessage = ModelConstants.UsernameErrorMessage)]
    public string? Username { get; set; }

    [RegularExpression(ModelConstants.PasswordPattern, ErrorMessage = ModelConstants.PasswordErrorMessage)]
    public string? Password { get; set; }

    [RegularExpression(ModelConstants.RolePattern, ErrorMessage = ModelConstants.RoleErrorMessage)]
    public UserRole Role { get; set; }
}