namespace DriveClone.Domain.Repositories;

public interface IUserExtensionRepository
{
    Task<(bool EmailUnique, bool UsernameUnique)> CheckUsernameAndEmailUniqueAsync(string username, string email);
}