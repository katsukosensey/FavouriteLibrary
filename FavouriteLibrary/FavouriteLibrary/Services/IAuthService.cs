using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Services
{
    interface IAuthService
    {
        Task<Result> Register(string name, string email, string password, string confirmationPassword);
        Task<Result<string>> Login(string email, string password);
        Task<Result<User>> GetMe();
        Task<Result> Logout();
    }
}
