using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Services
{
    class AuthorService : IAuthorService
    {
        public Task<Result<ICollection<Author>>> Get()
        {
            return Task.Run(() => new Result<ICollection<Author>>
            {
                IsSuccess = true,
                Data = new List<Author>
                {
                    new Author
                    {
                        Id = 1, Name = "Чак Паланик",
                        Bio = "Cовременный американский писатель и фриланс-журналист",
                        BirthDate = new DateTime(1962, 02, 21),
                        Photo = "https://i.ibb.co/cK9rP4D/palanik.jpg"
                    }
                }
            });
        }

        public Task<Result<Author>> GetById(int id)
        {
            return Task.Run(() => new Result<Author>
            {
                IsSuccess = true,
                Data = new Author
                    {
                        Id = 1, Name = "Чак Паланик",
                        Bio = "Cовременный американский писатель и фриланс-журналист",
                        BirthDate = new DateTime(1962, 02, 21),
                        Photo = "https://i.ibb.co/cK9rP4D/palanik.jpg"
                }
                
            });
        }
    }
}
