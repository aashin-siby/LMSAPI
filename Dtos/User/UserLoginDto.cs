using System.ComponentModel.DataAnnotations;
using LMSAPI.Utilities;

namespace LMSAPI.DTO;

//Dto to map when user login with User model
public class UserLoginDto
{

     [Required]
     public string Username { get; set; } = string.Empty;

     [Required]
     public string Password { get; set; } = string.Empty;
}