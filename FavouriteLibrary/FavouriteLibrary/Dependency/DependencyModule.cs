using System;
using System.Net.Http;
using Autofac;
using FavouriteLibrary.Api;
using FavouriteLibrary.Services;

namespace FavouriteLibrary.Dependency
{
    class DependencyModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new HttpClient{BaseAddress = new Uri(@"https://mobile.fakebook.press/")}).As<HttpClient>().SingleInstance();

            builder.RegisterType<AuthApiClient>().As<IAuthApiClient>().SingleInstance();
            builder.RegisterType<AuthorApiClient>().As<IAuthorApiClient>().SingleInstance();
            builder.RegisterType<BooksApiClient>().As<IBooksApiClient>().SingleInstance();

            builder.RegisterType<AuthService>().As<IAuthService>().SingleInstance();
            builder.RegisterType<AuthorService>().As<IAuthorService>().SingleInstance();
            builder.RegisterType<BookService>().As<IBookService>().SingleInstance();

            base.Load(builder);
        }
    }
}
