namespace DriveClone.Application.Common.Constants.ValidationMessages;

public static class AuthValidationErrorMessages
{
    public const string PasswordMinLength = "Password must be at least {0} characters long.";
    public const string InvalidEmailFormat = "Invalid email format.";
    public const string EmailTaken = "Email is already taken.";
    public const string PasswordSameAsOldOne = "Choose a password different from the current one.";
    
    // Register
    public const string InvalidEmail = "Invalid email format.";
    public const string InvalidLength = "Password must be at least {0} characters long.";
    public const string PasswordsDoNotMatch = "Passwords do not match.";
    public const string UpperCaseRequired = "Password must contain at least one uppercase letter.";
    public const string UserAlreadyExists = "User with this email already exists.";
    public const string DigitRequired = "Password must contain at least one digit.";
    public const string SpecialCharacterRequired = "Password must contain at least one special character.";
    public const string UsernameExists = "User with this email already exists.";
    public const string UsernameMustStartWithLetter = "Username must begin with a letter.";
    public const string UsernameNoSpecialCharacters = "Username must not contain special characters.";
    
    // Login
    public const string InvalidCredentials = "Invalid email or password.";
    public const string UserNotFound = "User not found.";
    public const string IncorrectPassword = "Incorrect password.";
    public const string TokenExpired = "Token has expired.";
    public const string TokenInvalid = "Token is invalid.";
    public const string RefreshTokenNotFound = "Refresh token not found.";
    public const string RefreshTokenInvalid = "Refresh token is invalid.";
    
    // Refresh Token
    public const string RefreshTokenRequired = "Refresh token is required.";
    public const string AccessTokenRequired = "Token is required.";
}
