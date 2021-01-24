using System.Collections.Generic;
using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Api
{
    interface IBooksApiClient
    {
        Task<Result<ICollection<Book>>> Get(bool needUpdate);
        Task<Result<ICollection<Book>>> GetFavourites(bool needUpdate);
        Task<Result<ICollection<Book>>> GetBooksByAuthor(int id, bool needUpdate);
        Task<Result> AddToFavourites(int id);
        Task<Result> RemoveFromFavourites(int id);
    }
}
