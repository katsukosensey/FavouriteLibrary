using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Services
{
    interface IAuthService
    {
        Task<Result<int>> Register(string name, string email, string password, string confirmationPassword);
        Task<Result<User>> Login(string email, string password);
        Task<Result<User>> GetMe();
        Task<Result<bool>> Logout();
    }
}
