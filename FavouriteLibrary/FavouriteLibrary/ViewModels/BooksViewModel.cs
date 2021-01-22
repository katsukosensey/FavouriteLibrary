﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using FavouriteLibrary.Models;
using FavouriteLibrary.Services;
using Xamarin.Forms;

namespace FavouriteLibrary.ViewModels
{
    class BooksViewModel : BaseViewModel
    {
        private IBookService bookService;
        private IAuthorService authorService;
        private IDialogService dialogService;
        public ObservableCollection<Book> Books { get; set; }
        public Command LoadBooksCommand { get; set; }
        public Command<Book> FavouriteStateChangedCommand { get; set; }

        public BooksViewModel()
        {
            bookService = new BookService();
            authorService = new AuthorService();
            dialogService = DependencyService.Get<IDialogService>();
            LoadBooksCommand = new Command(LoadBooks);
            FavouriteStateChangedCommand = new Command<Book>(ChangeFavouriteState);
            IsBusy = true;
            LoadBooks();
        }

        private async void ChangeFavouriteState(Book book)
        {
            Result<bool> result;
            if (book.IsFavourite)
            {
                result = await bookService.RemoveFromFavourites(book.Id);
            }
            else
            {
                result = await bookService.AddToFavourites(book.Id);
            }

            if (result.IsSuccess)
            {
                LoadBooks();
            }
            else
            {
                dialogService.ShowError(
                    ErrorStore.DataEditingFailureMessage,
                    ErrorStore.DataEditingFailure,
                    "Ok",
                    () => dialogService.CloseMessage());
            }
        }

        public async void LoadBooks()
        {
            var result = await bookService.Get();
            if (result.IsSuccess)
            {
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

                    result = await bookService.GetFavourites();
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
                     ErrorStore.DataLoadingFailureMessage, 
                    ErrorStore.DataLoadingFailure, 
                    "Ok", 
                    () => dialogService.CloseMessage());
            }
        }
    }
}
