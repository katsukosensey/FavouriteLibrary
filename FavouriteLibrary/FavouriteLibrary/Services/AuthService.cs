using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Services
{
    class AuthService : IAuthService
    {
        public Task<Result<int>> Register(string name, string email, string password, string confirmationPassword)
        {
            return Task.Run(()=>new Result<int> {IsSuccess = true});
        }

        public Task<Result<User>> Login(string email, string password)
        {
            return Task.Run(()=>new Result<User> {IsSuccess = true, Data = new User()
            {
                Id = 1,
                Name = "Ivan",
                Email = "aaa@gmail.com"
            }});

        }

        public Task<Result<User>> GetMe()
        {
            return Task.Run(() => new Result<User>
            {
                IsSuccess = true,
                Data = new User
                {
                    Id = 1,
                    Name = "Ivan",
                    Email = "aaa@gmail.com"
                }
            });
        }

        public Task<Result<bool>> Logout()
        {
            return Task.Run(() => new Result<bool> { IsSuccess = true });
        }
    }
}
