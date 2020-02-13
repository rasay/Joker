using System;
using System.Net;
using Newtonsoft.Json.Linq;

namespace JokerApp
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("****************************************************************");
            Console.WriteLine("Welcome to the 'Joker' app, home to the best jokes in the world!");
            Console.WriteLine("****************************************************************\n");

            while (true)
            {
                ProcessUserInput();
            }
        }

        private static void ProcessUserInput()
        {
            Console.Write("Enter 'r' to see a random joke, or 's' to search top 30: ");
            var input = Console.ReadLine().Trim().ToLower();
            if (input == "r")
                ShowRandomJoke();
            else if (input.StartsWith("s"))
            {
                input = "";
                while (input == "")
                {
                    Console.Write("Enter a search term: ");
                    input = Console.ReadLine().Trim();
                }
                ShowTop30Jokes(input);
            }
        }

        private static void ShowRandomJoke()
        {
            using (WebClient client = new WebClient())
            {
                Console.WriteLine(client.DownloadString("http://localhost:8080/jokes/randomjoke"));
            }
        }

        private static void ShowTop30Jokes(string searchTerm)
        {
            using (WebClient client = new WebClient())
            {
                string json = client.DownloadString("http://localhost:8080/jokes/top30search?term=" + searchTerm);
                DisplayTop30Json(json);
            }
        }

        private static void DisplayTop30Json(string json)
        {
            var parsedObject = JObject.Parse(json);

            Console.WriteLine("****************************************************************");
            DisplayJokeSection("Short Jokes", parsedObject.SelectToken("shortJokes"));
            DisplayJokeSection("Medium Jokes", parsedObject.SelectToken("mediumJokes"));
            DisplayJokeSection("Long Jokes", parsedObject.SelectToken("longJokes"));
            Console.WriteLine("****************************************************************");
        }

        private static void DisplayJokeSection(string label, JToken parsedObject)
        {
            Console.WriteLine("****** " + label + " ******");
            foreach (var jokeObject in parsedObject)
            {
                Console.WriteLine("\t" + jokeObject.ToString());
            }
            Console.WriteLine();
        }
    }
}
