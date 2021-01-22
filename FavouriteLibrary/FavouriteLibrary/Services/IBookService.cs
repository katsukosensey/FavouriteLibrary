using System.Collections.Generic;
using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Services
{
    interface IBookService
    {
        Task<Result<ICollection<Book>>> Get();
        Task<Result<ICollection<Book>>> GetFavourites();
        Task<Result<ICollection<Book>>> GetBooksByAuthor(int id);
        Task<Result<bool>> AddToFavourites(int id);
        Task<Result<bool>> RemoveFromFavourites(int id);
    }
}
