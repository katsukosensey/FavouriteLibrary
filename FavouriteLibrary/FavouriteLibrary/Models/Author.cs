using Newtonsoft.Json;

namespace FavouriteLibrary.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [JsonProperty("birth_date")]
        public string BirthDate { get; set; }
        [JsonProperty("died_date")]
        public string DeathDate { get; set; }
        public string Bio { get; set; }
        [JsonProperty("image")]
        public string Photo { get; set; }
        public int BooksCount { get; set; }
    }
}
