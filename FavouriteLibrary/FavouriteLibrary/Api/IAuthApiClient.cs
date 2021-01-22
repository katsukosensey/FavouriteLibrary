using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Api
{
    interface IAuthApiClient
    {
        Task<Result> Register(string name, string email, string password, string confirmationPassword);
        Task<Result<string>> Login(string email, string password, string confirmationPassword);
        Task<Result<User>> GetMe();
        Task<Result> Logout(string token);
        void SetToken(string token);
        void ReleaseToken();
    }
}
