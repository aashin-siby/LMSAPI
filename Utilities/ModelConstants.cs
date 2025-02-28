namespace LMSAPI.Utilities;

//Constants that are used in the DTOs for validation 
public static class ModelConstants
{

     // Regular Expression Patterns
     public const string UsernamePattern = @"^[a-zA-Z][a-zA-Z0-9]{3,19}$";
     public const string PasswordPattern = @"^(?=.*[a-z])(?=.*[0-9])(?=.*[A-Z])(?=.*\W).{8,15}$"
;
     public const string RolePattern = "^(Admin|User)$";
     public const string AuthorPattern = @"^[\p{L}\s]+$";
     public const string CopiesAvailablePattern = @"^\d+$";


     // Error Messages for User and Book
     public const string UsernameErrorMessage = "Username must start with a letter and contain only alphanumeric characters (3-20 characters long).";
     public const string PasswordErrorMessage =  "Password must be 8-15 characters long, include at least one uppercase letter, one lowercase letter, one number, and one special character.";
     public const string RoleErrorMessage = "Role must be either Admin or User";
     public const string CopiesAvailableErrorMessage = "CopiesAvailable must be a non-negative integer.";
     public const string AuthorErrorMessage = "Author can only contain letters and spaces.";

}