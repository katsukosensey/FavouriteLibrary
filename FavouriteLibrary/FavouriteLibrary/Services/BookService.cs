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

        public bool BooksChanged { get; set; }

        public Task<Result<ICollection<Book>>> Get(bool needUpdate)
        {
            return client.Get(needUpdate);
        }

        public Task<Result<ICollection<Book>>> GetFavourites(bool needUpdate)
        {
            return client.GetFavourites(needUpdate);
        }

        public Task<Result<ICollection<Book>>> GetBooksByAuthor(int id, bool needUpdate)
        {
            return client.GetBooksByAuthor(id, needUpdate);
        }

        public Task<Result> AddToFavourites(int id)
        {
            BooksChanged = true;
            return client.AddToFavourites(id);
        }

        public Task<Result> RemoveFromFavourites(int id)
        {
            BooksChanged = true;
            return client.RemoveFromFavourites(id);
        }
    }
}
