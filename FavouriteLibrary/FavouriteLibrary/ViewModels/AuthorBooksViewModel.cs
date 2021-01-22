using System.Collections.ObjectModel;
using CommonServiceLocator;
using FavouriteLibrary.Models;
using FavouriteLibrary.Services;
using Xamarin.Forms;

namespace FavouriteLibrary.ViewModels
{
    class AuthorBooksViewModel : BaseViewModel
    {

        private IBookService bookService;
        private IDialogService dialogService;
        public ObservableCollection<Book> Books { get; set; }

        public AuthorBooksViewModel()
        {
            bookService = ServiceLocator.Current.GetInstance<IBookService>();
            dialogService = DependencyService.Get<IDialogService>();
        }

        public async void LoadBooks(Author autor)
        {
            var result = await bookService.GetBooksByAuthor(autor.Id);
            if (result.IsSuccess)
            {
                var books = result.Data;
                if (books == null || books.Count == 0)
                {
                    Books = new ObservableCollection<Book>();
                }
                else
                {
                    Books = new ObservableCollection<Book>(books);
                }

                OnPropertyChanged(nameof(Books));
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
