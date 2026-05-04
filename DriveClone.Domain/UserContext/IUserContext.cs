namespace DriveClone.Domain.UserContext;

public interface IUserContext
{
    Guid UserId { get; }
    bool IsAuthenticated { get; }
}
