using FavouriteLibrary.Views;
using Xamarin.Forms;

namespace FavouriteLibrary
{
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("registration", typeof(RegistrationPage));
            Routing.RegisterRoute(nameof(AuthorDetailsPage), typeof(AuthorDetailsPage));
        }

    }
}
