using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CommonServiceLocator;
using FavouriteLibrary.Models;
using FavouriteLibrary.Services;
using MonkeyCache.FileStore;
using Newtonsoft.Json;
using Xamarin.Essentials;

namespace FavouriteLibrary.Api
{
    class BooksApiClient : IBooksApiClient
    {
        private HttpClient client;
        public BooksApiClient()
        {
            client = ServiceLocator.Current.GetInstance<HttpClient>();
        }
        public async Task<Result<ICollection<Book>>> Get(bool needUpdate)
        {
            var key = "books";
            if (needUpdate || !Barrel.Current.Exists(key) || Barrel.Current.IsExpired(key))
            {
                try
                {

                    var response = await client.GetAsync("api/books");
                    var content = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        return new Result<ICollection<Book>>
                        {
                            IsSuccess = false,
                            Error = ErrorStore.DataLoadingFailureMessage
                        };
                    }

                    var result = JsonConvert.DeserializeObject<Result<ICollection<Book>>>(content);
                    Barrel.Current.Add(key, result.Data, TimeSpan.FromDays(1));
                    return result;
                }
                catch
                {
                    return new Result<ICollection<Book>>
                    {
                        IsSuccess = false,
                        Error = ErrorStore.NoInternet
                    };
                }
            }

            return new Result<ICollection<Book>> {Data = Barrel.Current.Get<ICollection<Book>>(key) };
        }

        public async Task<Result<ICollection<Book>>> GetFavourites(bool needUpdate)
        {
            var key = "favourite_books";
            if (needUpdate || !Barrel.Current.Exists(key) || Barrel.Current.IsExpired(key))
            {
                try
                {
                    var response = await client.GetAsync("api/favorite-books");
                    var content = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        return new Result<ICollection<Book>>
                        {
                            IsSuccess = false,
                            Error = ErrorStore.DataLoadingFailureMessage
                        };
                    }
                    var result = JsonConvert.DeserializeObject<Result<ICollection<Book>>>(content);
                    Barrel.Current.Add(key, result.Data, TimeSpan.FromDays(1));
                    return result;
                }
                catch
                {
                    return new Result<ICollection<Book>>
                    {
                        IsSuccess = false,
                        Error = ErrorStore.NoInternet
                    };
                }
            }

            return new Result<ICollection<Book>> { Data = Barrel.Current.Get<ICollection<Book>>(key) };
        }

        public async Task<Result<ICollection<Book>>> GetBooksByAuthor(int id, bool needUpdate)
        {
            var key = $"{id}_books";
            if (needUpdate || !Barrel.Current.Exists(key) || Barrel.Current.IsExpired(key))
            {
                try
                {
                    var response = await client.GetAsync($"api/authors/{id}/books");
                    var content = await response.Content.ReadAsStringAsync();
                    if (!response.IsSuccessStatusCode)
                    {
                        return new Result<ICollection<Book>>
                        {
                            IsSuccess = false,
                            Error = ErrorStore.DataLoadingFailureMessage
                        };
                    }

                    var result = JsonConvert.DeserializeObject<Result<ICollection<Book>>>(content);
                    Barrel.Current.Add(key, result.Data, TimeSpan.FromDays(1));
                    return result;
                }
                catch
                {
                    return new Result<ICollection<Book>>
                    {
                        IsSuccess = false,
                        Error = ErrorStore.NoInternet
                    };
                }
                
            }
            return new Result<ICollection<Book>> { Data = Barrel.Current.Get<ICollection<Book>>(key) };
        }

        public async Task<Result> AddToFavourites(int id)
        {
            try
            {
                var response = await client.PostAsync($"api/books/{id}/add-to-favorites", new StringContent(""));
                if (!response.IsSuccessStatusCode)
                {
                    return new Result
                    {
                        IsSuccess = false,
                        Error = ErrorStore.DataEditingFailureMessage
                    };
                }
                Barrel.Current.Empty("favourite_books");
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

        public async Task<Result> RemoveFromFavourites(int id)
        {
            try
            {
                var response = await client.PostAsync($"api/books/{id}/remove-from-favorites", new StringContent(""));
                if (!response.IsSuccessStatusCode)
                {
                    return new Result
                    {
                        IsSuccess = false,
                        Error = ErrorStore.DataEditingFailureMessage
                    };
                }
                Barrel.Current.Empty("favourite_books");
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
    }
}
