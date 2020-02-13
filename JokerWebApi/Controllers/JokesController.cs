using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using JokerWebApi.Models;
using Newtonsoft.Json.Linq;

namespace JokerWebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JokesController : ControllerBase
    {
        private readonly ILogger<JokesController> _logger;
        private readonly IIcanhazdadjokeClient _icanhazdadjokeClient;
        private readonly JokeProcessor _jokeProcessor = new JokeProcessor();

        public JokesController(ILogger<JokesController> logger, IIcanhazdadjokeClient icanhazdadjokeClient)
        {
            _logger = logger;
            _icanhazdadjokeClient = icanhazdadjokeClient;
        }

        /// <summary>
        /// Request a random joke from the icanhazdadjoke client. Parse and
        /// return just the joke portion from the JSON response.
        /// </summary>
        /// <returns></returns>
        [HttpGet("randomjoke")]
        public string GetRandomJoke()
        {
            _logger.LogInformation("Get random joke");
            try
            {
                var json = _icanhazdadjokeClient.GetRandomJoke();
                var parsedObject = JObject.Parse(json);
                return parsedObject.SelectToken("joke").Value<string>();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Attempting to get random joke");
                throw e;
            }
        }

        [HttpGet("top30search")]
        public SearchResults GetTop30Search(string term = "")
        {
            _logger.LogInformation("Search top 30 jokes '{0}'", term);

            try
            {
                var results = new SearchResults();
                var json = _icanhazdadjokeClient.GetTop30Search(term);
                var parsedObject = JObject.Parse(json);
                foreach (var token in parsedObject.SelectToken("results"))
                {
                    var joke = token.SelectToken("joke").Value<string>();
                    int jokeLength = _jokeProcessor.CountWords(joke);
                    string taggedJoke = _jokeProcessor.TagJoke(term, joke);

                    if (jokeLength >= SearchResults.LONG_THRESHOLD)
                        results.LongJokes.Add(taggedJoke);
                    else if (jokeLength >= SearchResults.MEDIUM_THRESHOLD)
                        results.MediumJokes.Add(taggedJoke);
                    else
                        results.ShortJokes.Add(taggedJoke);
                }
                return results;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Attempting top 30 search for term '{0}'", term);
                throw e;
            }
        }
    }
}
