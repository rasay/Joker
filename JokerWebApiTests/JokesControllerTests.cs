using Moq;
using NUnit.Framework;

using JokerWebApi.Controllers;
using Microsoft.Extensions.Logging;

namespace JokerWebApiTests
{
    public class Tests
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
    }
}