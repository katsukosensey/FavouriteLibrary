using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
using FavouriteLibrary.Services;

namespace FavouriteLibrary.Dependency
{
    class DependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<Client>().As<Client>().SingleInstance();
            builder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();
            builder.RegisterType<AuthorService>().As<IAuthorService>().SingleInstance();
            builder.RegisterType<BookService>().As<IBookService>().SingleInstance();

            base.Load(builder);
        }
    }
}
