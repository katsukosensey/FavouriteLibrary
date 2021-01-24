using System.Collections.Generic;
using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Services
{
    interface IAuthorService
    {
        Task<Result<ICollection<Author>>> Get(bool needUpdate);
        Task<Result<Author>> GetById(int id);
    }
}
