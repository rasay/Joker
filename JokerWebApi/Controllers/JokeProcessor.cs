using System;
using System.Text.RegularExpressions;

namespace JokerWebApi.Controllers
{
    /// <summary>
    /// Performs processing operations on joke text (i.e. counting words
    /// and tagging occurences of search terms with []).
    /// </summary>
    public class JokeProcessor
    {
        public JokeProcessor()
        {
        }

        public int CountWords(string joke)
        {
            return Regex.Matches(joke, @"[A-Za-z0-9']+").Count;
        }

        public string TagJoke(string searchTerm, string joke)
        {
            var searchTermTagger = new MatchEvaluator(SearchTermTagger);
            return Regex.Replace(joke,
                string.Format(@"\b({0}[s]*)\b", searchTerm), searchTermTagger,
                RegexOptions.IgnoreCase);
        }

        private static string SearchTermTagger(Match match)
        {
            return string.Format("[{0}]", match.Value);
        }
    }
}
