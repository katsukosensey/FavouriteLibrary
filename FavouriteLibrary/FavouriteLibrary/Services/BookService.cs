using System.Collections.Generic;
using System.Threading.Tasks;
using CommonServiceLocator;
using FavouriteLibrary.Api;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Services
{
    class BookService : IBookService
    {
        private IBooksApiClient client;

        public BookService()
        {
            client = ServiceLocator.Current.GetInstance<IBooksApiClient>();
        }
        public Task<Result<ICollection<Book>>> Get()
        {
            return client.Get();
        }

        public Task<Result<ICollection<Book>>> GetFavourites(string token)
        {
            return client.GetFavourites(token);
        }

        public Task<Result<ICollection<Book>>> GetBooksByAuthor(int id)
        {
            return client.GetBooksByAuthor(id);
        }

        public Task<Result> AddToFavourites(int id, string token)
        {
            return client.AddToFavourites(id, token);
        }

        public Task<Result> RemoveFromFavourites(int id, string token)
        {
            return client.RemoveFromFavourites(id, token);
        }
    }
}
