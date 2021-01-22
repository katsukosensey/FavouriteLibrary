using System.Collections.ObjectModel;
using CommonServiceLocator;
using FavouriteLibrary.Models;
using FavouriteLibrary.Services;
using FavouriteLibrary.Views;
using Xamarin.Forms;

namespace FavouriteLibrary.ViewModels
{
    class AuthorsViewModel : BaseViewModel
    {
        private IBookService bookService;
        private IAuthorService authorService;
        private IDialogService dialogService;
        public ObservableCollection<Author> Authors { get; set; }
        public Command LoadAuthorsCommand { get; set; }
        public Command<Author> ShowDetailsCommand { get; set; }

        public AuthorsViewModel()
        {
            bookService = ServiceLocator.Current.GetInstance<IBookService>();
            authorService = ServiceLocator.Current.GetInstance<IAuthorService>();
            dialogService = DependencyService.Get<IDialogService>();
            LoadAuthorsCommand = new Command(LoadAuthors);
            ShowDetailsCommand = new Command<Author>(ShowDetails);
            IsBusy = true;
            LoadAuthors();
        }

        private async void ShowDetails(Author author)
        {
            if (author == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(AuthorDetailsPage)}?{nameof(AuthorDetailsViewModel.AuthorId)}={author.Id}");
        }

        public async void LoadAuthors()
        {
            var result = await authorService.Get();
            if (result.IsSuccess)
            {
                var authors = result.Data;
                if (authors == null || authors.Count == 0)
                {
                    Authors = new ObservableCollection<Author>();
                }
                else
                {
                    foreach (var author in authors)
                    {
                        var booksResult = await bookService.GetBooksByAuthor(author.Id);
                        if (booksResult.IsSuccess)
                        {
                            author.BooksCount = booksResult.Data.Count;
                        }
                        else
                        {
                            dialogService.ShowError(
                                ErrorStore.DataLoadingFailureMessage,
                                ErrorStore.DataLoadingFailure,
                                "Ok",
                                () => dialogService.CloseMessage());
                        }
                    }

                    Authors = new ObservableCollection<Author>(authors);
                }

                OnPropertyChanged(nameof(Authors));
                IsBusy = false;
            }
            else
            {
                IsBusy = false;
                dialogService.ShowError(
                    ErrorStore.DataLoadingFailureMessage,
                    ErrorStore.DataLoadingFailure,
                    "Ok",
                    () => dialogService.CloseMessage());
            }
        }
    }
}
