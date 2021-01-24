using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using FavouriteLibrary.Models;
using FavouriteLibrary.Services;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Essentials;

namespace FavouriteLibrary.Api
{
    class AuthApiClient : IAuthApiClient
    {
        private HttpClient client;
        public AuthApiClient()
        {
            client = ServiceLocator.Current.GetInstance<HttpClient>();
        }
        public async Task<Result> Register(string name, string email, string password, string confirmationPassword)
        {
            try
            {
                var response = await client.PostAsync("api/register", new StringContent(JsonConvert.SerializeObject(new
                {
                    name,
                    email,
                    password,
                    password_confirmation = confirmationPassword
                }), Encoding.UTF8, "application/json"));
                if (!response.IsSuccessStatusCode)
                {
                    return new Result
                    {
                        IsSuccess = false,
                        Error = ErrorStore.RegisterError
                    };
                }

                return new Result();
            }
            catch
            {
                return new Result
                {
                    IsSuccess = false,
                    Error = ErrorStore.NoInternet
                };
            }
        }

        public async Task<Result<string>> Login(string email, string password, string confirmationPassword)
        {
            try
            {

                var response = await client.PostAsync("api/login", new StringContent(JsonConvert.SerializeObject(new
                {
                    email,
                    password,
                    password_confirmation = confirmationPassword
                }), Encoding.UTF8, "application/json"));
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Result<string>
                    {
                        IsSuccess = false,
                        Error = ErrorStore.LoginError
                    };
                }

                var token = JObject.Parse(content).SelectToken("data.token")?.Value<string>();
                SetToken(token);
                return new Result<string>
                {
                    IsSuccess = true,
                    Data = token
                };
            }
            catch
            {
                return new Result<string>
                {
                    IsSuccess = false,
                    Error = ErrorStore.NoInternet
                };
            }
        }

        public async Task<Result<User>> GetMe()
        {
            var key = "me";
            if (!Barrel.Current.Exists(key) || Barrel.Current.IsExpired(key))
            {
                try
                {
                    var response = await client.GetAsync("api/me");
                    var content = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        return new Result<User>
                        {
                            IsSuccess = false,
                            Error = ErrorStore.DataLoadingFailureMessage
                        };
                    }

                    var result = JsonConvert.DeserializeObject<Result<User>>(content);
                    Barrel.Current.Add(key, result.Data, TimeSpan.FromDays(1));
                    return result;
                }
                catch
                {
                    return new Result<User>
                    {
                        IsSuccess = false,
                        Error = ErrorStore.NoInternet
                    };
                }
            }

            return new Result<User> { Data = Barrel.Current.Get<User>(key) };
            
        }

        public async Task<Result> Logout()
        {
            try
            {
                var response = await client.PostAsync("api/logout", new StringContent(""));
                if (!response.IsSuccessStatusCode)
                {
                    return new Result
                    {
                        IsSuccess = false,
                        Error = ErrorStore.DataLoadingFailureMessage
                    };
                }

                ReleaseToken();

                return new Result();
            }
            catch
            {
                return new Result
                {
                    IsSuccess = false,
                    Error = ErrorStore.NoInternet
                };
            }
        }

        public void SetToken(string token)
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", token);
        }

        public void ReleaseToken()
        {
            client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", string.Empty);
            Barrel.Current.Empty("favourite_books");
        }
    }
}
