using login_mvc_jwt.Dto.Users;
using login_mvc_jwt.Models;
namespace login_mvc_jwt.repository.Users
{
    public interface IRepositoryUsers
    {

        Task<UsersModels> register(registerDto registerDto);

        Task<UsersModels> login (loginDto loginDto);

    }
}
