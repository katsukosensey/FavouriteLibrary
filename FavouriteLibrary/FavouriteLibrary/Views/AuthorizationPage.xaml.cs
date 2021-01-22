using FavouriteLibrary.ViewModels;
using Xamarin.Forms.Xaml;

namespace FavouriteLibrary.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthorizationPage
    {
        public AuthorizationPage()
        {
            InitializeComponent();
            BindingContext = new AuthorizationViewModel();
        }
    }
}