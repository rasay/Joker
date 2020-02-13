using Moq;
using NUnit.Framework;

using JokerWebApi.Controllers;
using Microsoft.Extensions.Logging;
using JokerWebApi.Models;

namespace JokerWebApiTests
{
    public class JokesControllerTests
    {
        private readonly ILogger<JokesController> _mockLogger = new Mock<ILogger<JokesController>>().Object;

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestGetRandomJoke()
        {
            Mock<IIcanhazdadjokeClient> mockClient = new Mock<IIcanhazdadjokeClient>();
            mockClient.Setup(x => x.GetRandomJoke()).Returns("{\"id\":\"mGeFlykbFBd\",\"joke\":\"What do you call a sheep with no legs? A cloud.\",\"status\":200}");

            var controller = new JokesController(_mockLogger, mockClient.Object);
            var result = controller.GetRandomJoke();
            Assert.AreEqual("What do you call a sheep with no legs? A cloud.", result);
        }


        /// <summary>
        /// Method should parse out the following 3 jokes:
        /// 
        /// How did the hipster burn the roof of his mouth? He ate the pizza before it was cool.
        /// How much does a hipster weigh? An instagram.
        /// How many hipsters does it take to change a lightbulb? Oh, it's a really obscure number. You've probably never heard of it.
        /// </summary>
        [Test]
        public void TestGetTop30Jokes()
        {
            var searchTerm = "hipster";
            Mock<IIcanhazdadjokeClient> mockClient = new Mock<IIcanhazdadjokeClient>();
            mockClient.Setup(x => x.GetTop30Search(searchTerm))
                .Returns("{\"current_page\":1,\"limit\":20,\"next_page\":1,\"previous_page\":1,\"results\":[{\"id\":\"xc21Lmbxcib\",\"joke\":\"How did the hipster burn the roof of his mouth? He ate the pizza before it was cool.\"},{\"id\":\"GlGBIY0wAAd\",\"joke\":\"How much does a hipster weigh? An instagram.\"},{\"id\":\"NRuHJYgaUDd\",\"joke\":\"How many hipsters does it take to change a lightbulb? Oh, it's a really obscure number. You've probably never heard of it.\"}],\"search_term\":\"hipster\",\"status\":200,\"total_jokes\":3,\"total_pages\":1}");

            var controller = new JokesController(_mockLogger, mockClient.Object);
            SortedJokes results = controller.GetTop30Search(searchTerm);

            Assert.AreEqual(1, results.ShortJokes.Count);
            Assert.AreEqual("How much does a [hipster] weigh? An instagram.", results.ShortJokes[0]);

            Assert.AreEqual(1, results.MediumJokes.Count);
            Assert.AreEqual("How did the [hipster] burn the roof of his mouth? He ate the pizza before it was cool.", results.MediumJokes[0]);

            Assert.AreEqual(1, results.LongJokes.Count);
            Assert.AreEqual("How many [hipsters] does it take to change a lightbulb? Oh, it's a really obscure number. You've probably never heard of it.", results.LongJokes[0]);
        }
    }
}