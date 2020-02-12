using System;
using System.Net;

namespace JokerApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the 'Joker' app, home to the best jokes in the world!");
            Console.WriteLine("Enter 'r' to see a random joke, or 's <terms>' to search top 30 ...");
            var input = Console.ReadLine();
            if (input.ToLower() == "r")
                ShowRandomJoke();
        }

        private static void ShowRandomJoke()
        {
            using (WebClient client = new WebClient())
            {
                Console.WriteLine(client.DownloadString("http://localhost:8080/jokes/randomjoke"));
            }
        }
    }
}
