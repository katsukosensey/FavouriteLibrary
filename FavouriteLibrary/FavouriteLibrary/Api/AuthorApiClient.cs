using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using CommonServiceLocator;
using FavouriteLibrary.Models;
using FavouriteLibrary.Services;
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
        public async Task<Result<ICollection<Author>>> Get()
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

            return JsonConvert.DeserializeObject<Result<ICollection<Author>>>(content);
        }

        public async Task<Result<Author>> GetById(int id)
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
    }
}
