﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using AsyncAwaitBestPractices.MVVM;
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
        private Author author;
        public ObservableCollection<Book> Books { get; set; }
        public AsyncCommand LoadBooksCommand { get; set; }

        public AuthorBooksViewModel()
        {
            bookService = ServiceLocator.Current.GetInstance<IBookService>();
            dialogService = DependencyService.Get<IDialogService>();
            LoadBooksCommand = new AsyncCommand(() => LoadBooks(author, true));
        }

        public async Task LoadBooks(Author author, bool needUpdate)
        {
            this.author = author;
            var result = await bookService.GetBooksByAuthor(author.Id, needUpdate);
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
