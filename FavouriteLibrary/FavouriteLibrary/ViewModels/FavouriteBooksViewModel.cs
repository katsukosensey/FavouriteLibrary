using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
using CommonServiceLocator;
using FavouriteLibrary.Models;
using FavouriteLibrary.Services;
using Xamarin.Forms;

namespace FavouriteLibrary.ViewModels
{
    class FavouriteBooksViewModel : BaseViewModel
    {
        public IBookService BookService { get; }
        private IAuthorService authorService;
        private IDialogService dialogService;
        public ObservableCollection<Book> Books { get; set; }
        public AsyncCommand LoadBooksCommand { get; set; }
        public Command<Book> RemoveCommand { get; set; }

        public FavouriteBooksViewModel()
        {
            BookService = ServiceLocator.Current.GetInstance<IBookService>();
            authorService = ServiceLocator.Current.GetInstance<IAuthorService>();
            dialogService = DependencyService.Get<IDialogService>();
            LoadBooksCommand = new AsyncCommand(()=>LoadBooks(true, true));
            RemoveCommand = new Command<Book>(RemoveFromFavouritesRequest); 
        }

        private void RemoveFromFavouritesRequest(Book book)
        {
            dialogService.ShowMessage($"Вы действительно хотите удалить книгу {book.AuthorName} \"{book.Name}\" из списка избранных?",
                "Подтверждение",
                "Да",
                "Отмена",
                async isConfirmed => 
                {
                    dialogService.CloseMessage();
                    if (isConfirmed)
                    {
                        await RemoveFromFavourites(book);
                    }
                });
        }
        private async Task RemoveFromFavourites(Book book)
        {
            Result result = await BookService.RemoveFromFavourites(book.Id);
            if (result.IsSuccess)
            {
                Books.Remove(book);
                OnPropertyChanged(nameof(Books));
            }
            else
            {
                dialogService.ShowError(
                    result.Error,
                    ErrorStore.DataEditingFailure,
                    "Ok",
                    () => dialogService.CloseMessage());
            }
        }

        public async Task LoadBooks(bool needUpdate, bool needInvalidate)
        {
            IsBusy = true;
            var result = await BookService.GetFavourites(needUpdate);
            if (result.IsSuccess)
            {
                if (!needInvalidate) return;
                var books = result.Data;
                if (books == null || books.Count == 0)
                {
                    Books = new ObservableCollection<Book>();
                }
                else
                {
                    var authorsIds = books.Select(x => x.AuthorId).Distinct();
                    var authors = new List<Author>();
                    foreach (var authorsId in authorsIds)
                    {
                        var authorResult = await authorService.GetById(authorsId);
                        authors.Add(result.IsSuccess ? authorResult.Data : new Author { Id = authorsId });
                    }
                    foreach (var book in books)
                    {
                        book.IsFavourite = true;
                        book.AuthorName = authors.FirstOrDefault(x => x.Id == book.AuthorId)?.Name;
                    }
                    Books = new ObservableCollection<Book>(books);
                }
                OnPropertyChanged(nameof(Books));
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

