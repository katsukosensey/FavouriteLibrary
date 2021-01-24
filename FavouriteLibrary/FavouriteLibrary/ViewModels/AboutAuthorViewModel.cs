namespace FavouriteLibrary.ViewModels
{
    class AboutAuthorViewModel : BaseViewModel
    {
        private string bio;

        public string Bio
        {
            get => bio;
            set => SetProperty(ref bio, value);
        }
    }
}
