using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DriveClone.Infrastructure.Persistence.Repositories;

public class UserExtensionRepository(DataContext context)
{
    public async Task<(bool EmailUnique, bool UsernameUnique)> CheckUsernameAndEmailUniqueAsync(string username,
        string email)
    {
        var conflict = await context.Users
            .Where(u => u.UserName == username || u.Email == email)
            .GroupBy(_ => 1) // Group everything into a single row
            .Select(g => new
            {
                UsernameTaken = g.Any(u => u.UserName == username),
                EmailTaken = g.Any(u => u.Email == email)
            })
            .FirstOrDefaultAsync();

        if (conflict == null)
            return (EmailUnique: true, UsernameUnique: true);
        
        return (
            EmailUnique: !conflict.EmailTaken, 
            UsernameUnique: !conflict.UsernameTaken
        );
    } 
}
