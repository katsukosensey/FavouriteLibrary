using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CommonServiceLocator;
using FavouriteLibrary.Models;
using FavouriteLibrary.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FavouriteLibrary.ViewModels
{
    class BooksViewModel : BaseViewModel
    {
        public IBookService BookService { get; }
        private IAuthorService authorService;
        private IDialogService dialogService;
        public ObservableCollection<Book> Books { get; set; }
        public Command LoadBooksCommand { get; set; }
        public Command<Book> FavouriteStateChangedCommand { get; set; }

        public BooksViewModel()
        {
            BookService = ServiceLocator.Current.GetInstance<IBookService>();
            authorService = ServiceLocator.Current.GetInstance<IAuthorService>();
            dialogService = DependencyService.Get<IDialogService>();
            LoadBooksCommand = new Command(()=>LoadBooks(true, true));
            FavouriteStateChangedCommand = new Command<Book>(ChangeFavouriteState); 
        }

        private async void ChangeFavouriteState(Book book)
        {
            var bookIsFavourite = book.IsFavourite;
            book.IsFavourite = !bookIsFavourite;
            var token = await SecureStorage.GetAsync("token");
            Result result;
            if (bookIsFavourite)
            {
                result = await BookService.RemoveFromFavourites(book.Id, token);
            }
            else
            {
                result = await BookService.AddToFavourites(book.Id, token);
            }

            if (!result.IsSuccess)
            {
                book.IsFavourite = !book.IsFavourite;
                dialogService.ShowError(
                    result.Error,
                    ErrorStore.DataEditingFailure,
                    "Ok",
                    () => dialogService.CloseMessage());
            }
        }

        public async void LoadBooks(bool needToUpdate, bool needToInvalidate)
        {
            IsBusy = true;
            var token = await SecureStorage.GetAsync("token");
            var result = await BookService.Get(needToUpdate);
            if (result.IsSuccess)
            {
                if (!needToInvalidate) return;
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
                        authors.Add(result.IsSuccess ? authorResult.Data : new Author {Id = authorsId});
                    }
                    foreach (var book in books)
                    {
                        book.AuthorName = authors.FirstOrDefault(x => x.Id == book.AuthorId)?.Name;
                    }

                    result = await BookService.GetFavourites(token, needToUpdate);
                    foreach (var book in books)
                    {
                        if (result.Data.Any(x => x.Id == book.Id))
                        {
                            book.IsFavourite = true;
                        }
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
