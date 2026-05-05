namespace DriveClone.Application.DTOs.AuthDtos;

public class CreateUserAppDto
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}