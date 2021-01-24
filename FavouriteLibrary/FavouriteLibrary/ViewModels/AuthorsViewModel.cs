using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
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
        public AsyncCommand LoadAuthorsCommand { get; set; }
        public AsyncCommand<Author> ShowDetailsCommand { get; set; }

        public AuthorsViewModel()
        {
            bookService = ServiceLocator.Current.GetInstance<IBookService>();
            authorService = ServiceLocator.Current.GetInstance<IAuthorService>();
            dialogService = DependencyService.Get<IDialogService>();
            LoadAuthorsCommand = new AsyncCommand(()=>LoadAuthors(true));
            ShowDetailsCommand = new AsyncCommand<Author>(ShowDetails);
            _ = LoadAuthors(false);
        }

        private async Task ShowDetails(Author author)
        {
            if (author == null)
                return;
            await Shell.Current.GoToAsync($"{nameof(AuthorDetailsPage)}?{nameof(AuthorDetailsViewModel.AuthorId)}={author.Id}");
        }

        public async Task LoadAuthors(bool needUpdate)
        {
            IsBusy = true;
            var result = await authorService.Get(needUpdate);
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
                        var booksResult = await bookService.GetBooksByAuthor(author.Id, needUpdate);
                        if (booksResult.IsSuccess)
                        {
                            author.BooksCount = booksResult.Data.Count;
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

                    Authors = new ObservableCollection<Author>(authors);
                }

                OnPropertyChanged(nameof(Authors));
                IsBusy = false;
            }
            else
            {
                IsBusy = false;
                dialogService.ShowError(
                    result.Error,
                    ErrorStore.DataLoadingFailure,
                    "Ok",
                    () => dialogService.CloseMessage());
            }
        }
    }
}
