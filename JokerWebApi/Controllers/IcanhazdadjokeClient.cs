using System;
using System.Net;
using System.Net.Http;

namespace JokerWebApi.Controllers
{
    public interface IIcanhazdadjokeClient
    {
        string GetRandomJoke();
    }

    /// <summary>
    /// Send REST API calls to icanhazdadjoke service and return JSON
    /// formatted response.
    /// </summary>
    public class IcanhazdadjokeClient : IIcanhazdadjokeClient
    {
        private static readonly HttpClient client = new HttpClient();

        public IcanhazdadjokeClient()
        {
        }

        public string GetRandomJoke()
        {
            using (var client = new WebClient())
            {
                client.Headers.Add("Accept", "application/json");
                client.Headers.Add("User-Agent", "SteelyDan");
                return client.DownloadString("https://icanhazdadjoke.com/");
            }
        }
    }
}
