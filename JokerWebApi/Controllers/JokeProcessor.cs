using System;
using System.Text.RegularExpressions;

namespace JokerWebApi.Controllers
{
    public class JokeProcessor
    {
        public JokeProcessor()
        {
        }

        public int CountWords(string joke)
        {
            return Regex.Matches(joke, @"[A-Za-z0-9'\.]+").Count;
        }

        public string TagJoke(string searchTerm, string joke)
        {
            var searchTermTagger = new MatchEvaluator(SearchTermTagger);
            return Regex.Replace(joke,
                string.Format(@"\b({0})", searchTerm), searchTermTagger,
                RegexOptions.IgnoreCase);
        }

        private static string SearchTermTagger(Match match)
        {
            return string.Format("[{0}]", match.Value);
        }
    }
}
