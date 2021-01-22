namespace FavouriteLibrary.Models
{
    class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }
        public T Data { get; set; }
    }
}
