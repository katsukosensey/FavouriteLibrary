using FavouriteLibrary.ViewModels;
using Xamarin.Forms.Xaml;

namespace FavouriteLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BooksPage
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
                _ = btx.LoadBooks(hasChanged, hasChanged || btx.Books != null);
                btx.BookService.BooksChanged = false;
            }
        }
    }
}