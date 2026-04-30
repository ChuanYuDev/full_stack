namespace UseCases.UsersServiceInterfaces;

public interface IUsersService
{
    Task<string> GetUserId();
}