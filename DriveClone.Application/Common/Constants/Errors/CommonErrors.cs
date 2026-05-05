using DriveClone.Application.Common.ErrorAndResults;

namespace DriveClone.Application.Common.Constants.Errors;

public class CommonErrors
{
    public static Error CannotGenerateToken(string? customMessage = null) 
        => new("CannotGenerateToken", customMessage ?? "Unable to login at this time. Please try again later.");
    
    public static Error InternalServerError(string? customMessage = null) 
        => new("InternalServerError", customMessage ?? "An internal server error occurred. Please try again later.");
    
    public static Error InvalidRefreshToken(string? customMessage = null) 
        => new("InvalidRefreshToken", customMessage ?? "The provided token is invalid or has expired.");
    
    public static Error ValidationProblem(Dictionary<string, string[]> errors, string? customMessage = null)
    => new ("ValidationProblem", customMessage ?? "One or more validation error occured",  errors);
    
}
