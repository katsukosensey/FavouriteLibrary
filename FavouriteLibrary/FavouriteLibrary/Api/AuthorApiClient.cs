using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CommonServiceLocator;
using FavouriteLibrary.Models;
using FavouriteLibrary.Services;
using MonkeyCache.FileStore;
using Newtonsoft.Json;

namespace FavouriteLibrary.Api
{
    class AuthorApiClient : IAuthorApiClient
    {
        private HttpClient client;
        public AuthorApiClient()
        {
            client = ServiceLocator.Current.GetInstance<HttpClient>();
        }
        public async Task<Result<ICollection<Author>>> Get(bool needUpdate)
        {
            var key = "authors";
            if (needUpdate || !Barrel.Current.Exists(key) || Barrel.Current.IsExpired(key))
            {
                try
                {
                    var response = await client.GetAsync("api/authors");
                    var content = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        return new Result<ICollection<Author>>
                        {
                            IsSuccess = false,
                            Error = ErrorStore.DataLoadingFailureMessage
                        };
                    }

                    var result = JsonConvert.DeserializeObject<Result<ICollection<Author>>>(content);
                    Barrel.Current.Add(key, result.Data, TimeSpan.FromDays(1));
                    return result;
                }
                catch
                {
                    return new Result<ICollection<Author>>
                    {
                        IsSuccess = false,
                        Error = ErrorStore.NoInternet
                    };
                }
            }

            return new Result<ICollection<Author>> { Data = Barrel.Current.Get<ICollection<Author>>(key) };
        }

        public async Task<Result<Author>> GetById(int id)
        {
            try
            {
                var response = await client.GetAsync($"api/authors/{id}");
                var content = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    return new Result<Author>
                    {
                        IsSuccess = false,
                        Error = ErrorStore.DataLoadingFailureMessage
                    };
                }

                return JsonConvert.DeserializeObject<Result<Author>>(content);
            }
            catch
            {
                return new Result<Author>
                {
                    IsSuccess = false,
                    Error = ErrorStore.NoInternet
                };
            }
        }
    }
}
