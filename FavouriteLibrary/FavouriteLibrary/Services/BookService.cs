using System.Collections.Generic;
using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Services
{
    class BookService : IBookService
    {
        public Task<Result<ICollection<Book>>> Get()
        {
            return Task.Run(() => new Result<ICollection<Book>>
            {
                IsSuccess = true,
                Data = new List<Book>
                {
                    new Book
                    {
                        AuthorId = 1, Id = 1, Name = "Уцелевший",
                        ImagePath = "https://i.ibb.co/P4ksZrz/palanik-survivor.jpg"
                    },
                    new Book
                    {
                        AuthorId = 1, Id = 2, Name = "Колыбельная",
                        ImagePath = "https://i.ibb.co/prMHsst/palanik-lullaby.jpg"
                    },
                }
            });
        }

        public Task<Result<ICollection<Book>>> GetFavourites()
        {
            return Task.Run(() => new Result<ICollection<Book>>
            {
                IsSuccess = true,
                Data = new List<Book>
                {
                    new Book
                    {
                        AuthorId = 1, Id = 1, Name = "Уцелевший",
                        ImagePath = "https://i.ibb.co/P4ksZrz/palanik-survivor.jpg"
                    }
                }
            });
        }

        public Task<Result<ICollection<Book>>> GetBooksByAuthor(int id)
        {
            return Task.Run(() => new Result<ICollection<Book>>
            {
                IsSuccess = true,
                Data = new List<Book>
                {
                    new Book
                    {
                        AuthorId = 1, Id = 1, Name = "Уцелевший",
                        ImagePath = "https://i.ibb.co/P4ksZrz/palanik-survivor.jpg"
                    },
                    new Book
                    {
                        AuthorId = 1, Id = 2, Name = "Колыбельная",
                        ImagePath = "https://i.ibb.co/prMHsst/palanik-lullaby.jpg"
                    },
                }
            });
        }

        public Task<Result<bool>> AddToFavourites(int id)
        {
            return Task.Run(() => new Result<bool>() {IsSuccess = true, Data = true});
        }

        public Task<Result<bool>> RemoveFromFavourites(int id)
        {
            return Task.Run(() => new Result<bool>() { IsSuccess = true, Data = true });
        }
    }
}
