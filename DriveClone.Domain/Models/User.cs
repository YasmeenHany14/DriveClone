using Microsoft.AspNetCore.Identity;

namespace DriveClone.Domain.Models;

public class User : IdentityUser
{    
    public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
}
