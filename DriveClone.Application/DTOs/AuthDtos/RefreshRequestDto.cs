namespace DriveClone.Application.DTOs.AuthDtos;

public class RefreshRequestDto
{
    public string AccessToken { get; init; }
    public string RefreshToken { get; init; }
}
