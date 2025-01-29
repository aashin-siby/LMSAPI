namespace LMSAPI.Utilities;

//Constants that are used in the DTOs for validation 
public static class ModelConstants
{

     // Regular Expression Patterns
     public const string UsernamePattern = @"^[a-zA-Z][a-zA-Z0-9]{3,19}$";
     public const string PasswordPattern = @"^[a-zA-Z0-9!@#$%^&*()_+]+$";
     public const string RolePattern = "^(Admin|User)$";
     public const string AuthorPattern = @"^[\p{L}\s]+$";
     public const string CopiesAvailablePatter = @"^\d+$";


     // Error Messages for User and Book
     public const string UsernameErrorMessage = "Username must start with a letter and contain only alphanumeric characters (3-20 characters long).";
     public const string PasswordErrorMessage = "Password can contain alphanumeric characters and special characters like !@#$%^&*()_+.";
     public const string RoleErrorMessage = "Role must be either Admin or User";
     public const string CopiesAvailableErrorMessage = "CopiesAvailable must be a non-negative integer.";
     public const string AuthorErrorMessage = "Author can only contain letters and spaces.";

}