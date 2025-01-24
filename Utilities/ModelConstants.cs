namespace LMSAPI.Utilities;


public static class ModelConstants
{
     // Regular Expression Patterns

     public const string UsernamePatter = @"^[a-zA-Z0-9]+$";
     public const string PasswordPattern = @"^[a-zA-Z0-9!@#$%^&*()_+]+$";
     public const string RolePattern = "^(0|1)$";
     public const string AuthorPattern = @"^[\p{L}\s]+$";
     public const string CopiesAvailablePatter = @"^\d+$";


     // Error Messages for User and Book
     public const string UsernameErrorMessage = "Username can only contain alphanumeric characters.";
     public const string PasswordErrorMessage = "Password can contain alphanumeric characters and special characters like !@#$%^&*()_+.";
     public const string RoleErrorMessage = "Role must be either Admin or User";
     public const string CopiesAvailableErrorMessage = "CopiesAvailable must be a non-negative integer.";
     public const string AuthorErrorMessage = "Author can only contain letters and spaces.";


}