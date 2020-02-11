using System;
using System.Collections.Generic;

namespace JokerWebApi.Models
{
    public class SearchResults
    {
        // < 10 words
        public List<string> ShortJokes { get; set; }

        // < 20 words
        public List<string> MediumJokes { get; set; }

        // >= 20 words
        public List<string> LongJokes { get; set; }
    }
}
