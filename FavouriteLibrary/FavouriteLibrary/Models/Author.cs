namespace FavouriteLibrary.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public System.DateTime BirthDate { get; set; }
        public System.DateTime DeathDate { get; set; }
        public string Bio { get; set; }
        public string Photo { get; set; }
        public int BooksCount { get; set; }
    }
}
