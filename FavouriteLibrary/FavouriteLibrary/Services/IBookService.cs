using System.Collections.Generic;
using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Services
{
    interface IBookService
    {
        /// <summary>
        /// изменилось свойство избранности
        /// </summary>
        bool BooksChanged { get; set; }
        Task<Result<ICollection<Book>>> Get(bool needUpdate);
        Task<Result<ICollection<Book>>> GetFavourites(bool needUpdate);
        Task<Result<ICollection<Book>>> GetBooksByAuthor(int id, bool needUpdate);
        Task<Result> AddToFavourites(int id);
        Task<Result> RemoveFromFavourites(int id);
    }
}
