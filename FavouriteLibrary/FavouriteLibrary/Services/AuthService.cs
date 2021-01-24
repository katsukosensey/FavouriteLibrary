using System.Threading.Tasks;
using CommonServiceLocator;
using FavouriteLibrary.Api;
using FavouriteLibrary.Models;
using Xamarin.Essentials;

namespace FavouriteLibrary.Services
{
    class AuthService : IAuthService
    {
        private readonly IAuthApiClient client;

        public AuthService()
        {
            client = ServiceLocator.Current.GetInstance<IAuthApiClient>();
            CheckToken();
        }
        private async void CheckToken()
        {
            var token = await SecureStorage.GetAsync("token");
            if (token != null)
            {
                client.SetToken(token);
            }
            else
            {
                client.ReleaseToken();
            }
        }

        public Task<Result> Register(string name, string email, string password, string confirmationPassword)
        {
            return client.Register(name, email, password, confirmationPassword);
        }

        public Task<Result<string>> Login(string email, string password)
        {
            return client.Login(email, password, password);

        }

        public Task<Result<User>> GetMe()
        {
            return client.GetMe();
        }

        public Task<Result> Logout()
        {
            return client.Logout();
        }
    }
}
