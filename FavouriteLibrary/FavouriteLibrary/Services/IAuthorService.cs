﻿using System.Collections.Generic;
using System.Threading.Tasks;
using FavouriteLibrary.Models;

namespace FavouriteLibrary.Services
{
    interface IAuthorService
    {
        Task<Result<ICollection<Author>>> Get();
        Task<Result<Author>> GetById(int id);
    }
}
