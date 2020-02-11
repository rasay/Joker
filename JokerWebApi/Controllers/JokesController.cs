using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        public String GetRandomJoke()
        {
            _logger.LogInformation("Get random joke");

            var json = _icanhazdadjokeClient.GetRandomJoke();
            var parsedObject = JObject.Parse(json);
            return parsedObject.SelectToken("joke").Value<string>();
        }
    }
}
