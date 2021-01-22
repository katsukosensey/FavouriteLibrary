using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CommonServiceLocator;
using FavouriteLibrary.Models;
using FavouriteLibrary.Services;
using Newtonsoft.Json;

namespace FavouriteLibrary.Api
{
    class BooksApiClient : IBooksApiClient
    {
        private HttpClient client;
        public BooksApiClient()
        {
            client = ServiceLocator.Current.GetInstance<HttpClient>();
        }
        public async Task<Result<ICollection<Book>>> Get()
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

            return JsonConvert.DeserializeObject<Result<ICollection<Book>>>(content);
        }

        public async Task<Result<ICollection<Book>>> GetFavourites(string token)
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

            return JsonConvert.DeserializeObject<Result<ICollection<Book>>>(content);
        }

        public async Task<Result<ICollection<Book>>> GetBooksByAuthor(int id)
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

            return JsonConvert.DeserializeObject<Result<ICollection<Book>>>(content);
        }

        public async Task<Result> AddToFavourites(int id, string token)
        {
            var response = await client.PostAsync($"api/books/{id}/add-to-favorites", new StringContent(""));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new Result
                {
                    IsSuccess = false,
                    Error = ErrorStore.DataEditingFailureMessage
                };
            }
            return new Result();
        }

        public async Task<Result> RemoveFromFavourites(int id, string token)
        {
            var response = await client.PostAsync($"api/books/{id}/remove-from-favorites", new StringContent(""));
            var content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                return new Result
                {
                    IsSuccess = false,
                    Error = ErrorStore.DataEditingFailureMessage
                };
            }
            return new Result();
        }
    }
}
