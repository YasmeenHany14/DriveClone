using DriveClone.Application.Common.ErrorAndResults;

namespace DriveClone.Application.Common.Constants.Errors;

public class AuthErrors
{
    public static Error WrongCredentials(string? customMessage = null) 
        => new("WrongCredentials", customMessage ?? "The provided credentials are incorrect.");
    public static readonly Error UserNotActive = new("UserNotActive", "The user account is currently disabled.");
    public static readonly Error Forbidden = new("Forbidden", "Forbidden.");
}
