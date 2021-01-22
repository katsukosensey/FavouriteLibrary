using System;
using FavouriteLibrary.ViewModels;
using Xamarin.Forms.Xaml;

namespace FavouriteLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorDetailsPage
    {
        public AuthorDetailsPage()
        {
            InitializeComponent();
            ((AuthorDetailsViewModel)BindingContext).UpdateAuthorAction += OnUpdateAuthorAction;
        }
        

        private void OnUpdateAuthorAction()
        {
            UpdateAuthorDetails();
        }

        private void AuthorDetailsPage_OnCurrentPageChanged(object sender, EventArgs e)
        {
            UpdateAuthorDetails();
        }

        private void UpdateAuthorDetails()
        {
            var author = ((AuthorDetailsViewModel)BindingContext).Author;
            if (author == null) return;
            if (CurrentPage is AboutAuthorPage)
            {
                ((AboutAuthorViewModel)CurrentPage.BindingContext).Bio = author.Bio;
            }
            if (CurrentPage is AuthorBooksPage)
            {
                ((AuthorBooksViewModel)CurrentPage.BindingContext).LoadBooks(author);
            }
        }
    }
}