using FavouriteLibrary.Services;
using FavouriteLibrary.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FavouriteLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksPage : ContentPage
    {
        public BooksPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var btx = (BooksViewModel) BindingContext;
            var hasChanged = btx.BookService.BooksChanged;
            if (hasChanged || btx.Books == null)
            {
                btx.LoadBooks(hasChanged, hasChanged || btx.Books != null);
                btx.BookService.BooksChanged = false;
            }
        }
    }
}