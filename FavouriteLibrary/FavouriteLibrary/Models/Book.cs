using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace FavouriteLibrary.Models
{
    class Book : INotifyPropertyChanged
    {
        private bool isFavourite;
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("author_id")]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        [JsonProperty("desc")]
        public string Description { get; set; }
        [JsonProperty("image")]
        public string ImagePath { get; set; }

        public bool IsFavourite
        {
            get => isFavourite;
            set
            {
                isFavourite = value;
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
