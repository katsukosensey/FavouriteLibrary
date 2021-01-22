namespace FavouriteLibrary.ViewModels
{
    class AboutAuthorViewModel : BaseViewModel
    {
        private string _bio;

        public string Bio
        {
            get => _bio;
            set
            {
                _bio = value;
                OnPropertyChanged();
            }
        }
    }
}
