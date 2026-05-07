namespace DriveClone.Application.DTOs.UserDtos;

public class UpdateUserAppDto : AppBaseDto
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Email { get; set; }
}
