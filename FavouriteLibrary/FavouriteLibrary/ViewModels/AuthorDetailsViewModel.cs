using System;
using CommonServiceLocator;
using FavouriteLibrary.Models;
using FavouriteLibrary.Services;
using Xamarin.Forms;

namespace FavouriteLibrary.ViewModels
{
    [QueryProperty(nameof(AuthorId), nameof(AuthorId))]
    class AuthorDetailsViewModel : BaseViewModel
    {
        private string _authorId;
        private IAuthorService authorService;
        public Action UpdateAuthorAction;
        public string AuthorId
        {
            get => _authorId;
            set
            {
                _authorId = value;
                InitAuthor();
            }
        }

        public Author Author { get; set; }

        public AuthorDetailsViewModel()
        {
            authorService = ServiceLocator.Current.GetInstance<IAuthorService>();
        }

        private async void InitAuthor()
        {
            var result = await authorService.GetById(int.Parse(AuthorId));
            if (result.IsSuccess)
            {
                Author = result.Data;
                OnPropertyChanged(nameof(Author));
                UpdateAuthorAction?.Invoke();
            }
        }
    }
}
