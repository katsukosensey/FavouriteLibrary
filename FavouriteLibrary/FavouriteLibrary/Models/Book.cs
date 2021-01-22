using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace FavouriteLibrary.Models
{
    class Book : INotifyPropertyChanged
    {
        private bool _isFavourite;
        public int Id { get; set; }
        public string Name { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string ImagePath { get; set; }

        public bool IsFavourite
        {
            get => _isFavourite;
            set
            {
                _isFavourite = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
