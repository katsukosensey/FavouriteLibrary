using FavouriteLibrary.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace FavouriteLibrary.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}