namespace DriveClone.Application.DTOs.UserDtos;

public class UpdatePasswordRequestAppDto : AppBaseDto
{
    public string CurrentPassword { get; set; }
    public string NewPassword { get; set; }
    public string ConfirmPassword { get; set; }
}
