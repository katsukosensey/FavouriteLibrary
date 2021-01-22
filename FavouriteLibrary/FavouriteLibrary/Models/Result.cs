namespace FavouriteLibrary.Models
{
    class Result
    {
        public bool IsSuccess { get; set; }
        public string Error { get; set; }

        public Result()
        {
            IsSuccess = true;
        }
    }
    class Result<T> : Result
    {
        public T Data { get; set; }
    }
}
