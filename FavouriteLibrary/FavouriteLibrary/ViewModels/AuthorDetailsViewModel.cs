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
        private string authorId;
        private IAuthorService authorService;
        private IDialogService dialogService;
        public Action UpdateAuthorAction;
        public string AuthorId
        {
            get => authorId;
            set
            {
                authorId = value;
                InitAuthor();
            }
        }

        public Author Author { get; set; }

        public AuthorDetailsViewModel()
        {
            authorService = ServiceLocator.Current.GetInstance<IAuthorService>();
            dialogService = DependencyService.Get<IDialogService>();
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
            else
            {
                dialogService.ShowError(
                    result.Error,
                    ErrorStore.DataLoadingFailure,
                    "Ok",
                    () => dialogService.CloseMessage());
            }
        }
    }
}
