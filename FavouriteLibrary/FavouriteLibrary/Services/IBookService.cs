using System;
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
        Task<Result<ICollection<Book>>> GetFavourites(string token, bool needUpdate);
        Task<Result<ICollection<Book>>> GetBooksByAuthor(int id, bool needUpdate);
        Task<Result> AddToFavourites(int id, string token);
        Task<Result> RemoveFromFavourites(int id, string token);
    }
}
