using System;
using System.Net;
using System.Net.Http;

namespace JokerWebApi.Controllers
{
    public interface IIcanhazdadjokeClient
    {
        string GetRandomJoke();

        string GetTop30Search(string searchTerm);
    }

    /// <summary>
    /// Send REST API calls to icanhazdadjoke service and return JSON
    /// formatted response.
    /// </summary>
    public class IcanhazdadjokeClient : IIcanhazdadjokeClient
    {
        private static string _userAgent = "My Library (https://github.com/rasay/Joker.git)";

        public IcanhazdadjokeClient()
        {
        }

        public string GetRandomJoke()
        {
            return GetResponse("https://icanhazdadjoke.com/");
        }

        public string GetTop30Search(string searchTerm)
        {
            return GetResponse(string.Format("https://icanhazdadjoke.com/search?term={0}&limit=30", searchTerm));
        }

        private string GetResponse(string url)
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("User-Agent", _userAgent);
                return client.DownloadString(url);
            }
        }
    }
}
