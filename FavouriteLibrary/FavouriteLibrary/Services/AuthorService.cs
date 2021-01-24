using System.Collections.Generic;
using System.Threading.Tasks;
using CommonServiceLocator;
using FavouriteLibrary.Api;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Services
{
    class AuthorService : IAuthorService
    {
        private IAuthorApiClient client;

        public AuthorService()
        {
            client = ServiceLocator.Current.GetInstance<IAuthorApiClient>();
        }
        public Task<Result<ICollection<Author>>> Get(bool needUpdate)
        {
            return client.Get(needUpdate);
        }

        public Task<Result<Author>> GetById(int id)
        {
            return client.GetById(id);
        }
    }
}
