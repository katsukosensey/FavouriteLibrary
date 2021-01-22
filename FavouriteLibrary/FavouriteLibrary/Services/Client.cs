using System.Net.Http;

namespace FavouriteLibrary.Services
{
    class Client
    {
        public HttpClient HttpClient { get; set; }

        public Client()
        {
            HttpClient = new HttpClient();
        }
    }
}
