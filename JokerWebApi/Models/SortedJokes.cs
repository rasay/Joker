using System;
using System.Collections.Generic;

namespace JokerWebApi.Models
{
    public class SortedJokes
    {
        public const int MEDIUM_THRESHOLD = 10;

        public const int LONG_THRESHOLD = 20;

        public SortedJokes()
        {
            ShortJokes = new List<string>();
            MediumJokes = new List<string>();
            LongJokes = new List<string>();
        }

        // < 10 words
        public List<string> ShortJokes { get; set; }

        // < 20 words
        public List<string> MediumJokes { get; set; }

        // >= 20 words
        public List<string> LongJokes { get; set; }
    }
}
