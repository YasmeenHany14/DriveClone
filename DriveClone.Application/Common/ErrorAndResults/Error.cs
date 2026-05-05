namespace DriveClone.Application.Common.ErrorAndResults;

public sealed record Error(string Code, string Description, Dictionary<string, string[]>? Errors = null)
{
    public static readonly Error None = new(string.Empty, string.Empty);
    //TODO: format error response later
}
