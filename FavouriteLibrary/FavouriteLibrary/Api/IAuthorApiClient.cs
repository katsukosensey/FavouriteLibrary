using System.Collections.Generic;
using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Api
{
    interface IAuthorApiClient
    {
        Task<Result<ICollection<Author>>> Get();
        Task<Result<Author>> GetById(int id);
    }
}
