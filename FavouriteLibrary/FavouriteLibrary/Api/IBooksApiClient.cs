using System.Collections.Generic;
using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Api
{
    interface IBooksApiClient
    {
        Task<Result<ICollection<Book>>> Get(bool needUpdate);
        Task<Result<ICollection<Book>>> GetFavourites(string token, bool needUpdate);
        Task<Result<ICollection<Book>>> GetBooksByAuthor(int id, bool needUpdate);
        Task<Result> AddToFavourites(int id, string token);
        Task<Result> RemoveFromFavourites(int id, string token);
    }
}
