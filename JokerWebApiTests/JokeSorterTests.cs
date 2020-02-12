using Moq;
using NUnit.Framework;

using JokerWebApi.Controllers;
using Microsoft.Extensions.Logging;
using JokerWebApi.Models;

namespace JokerWebApiTests
{
    public class JokeSorterTests
    {
        JokeSorter _jokeSorter;

        [SetUp]
        public void Setup()
        {
            _jokeSorter = new JokeSorter();
        }

        [Test]
        public void TestCountWordsSimple()
        {
            int actualCount = _jokeSorter.CountWords("one two three");
            Assert.AreEqual(3, actualCount);
        }

        [Test]
        public void TestCountWordsWithPunctuation()
        {
            int actualCount = _jokeSorter.CountWords(" one \t two's  three. four.");
            Assert.AreEqual(4, actualCount);
        }

        [Test]
        public void TestTagJokeSimple()
        {
            Assert.AreEqual("one [two] three", _jokeSorter.TagJoke("two", "one two three"));
        }

        [Test]
        public void TestTagJokeWithPunctuation()
        {
            Assert.AreEqual("[one] [one]'s loner two threeone [one]. ",
                _jokeSorter.TagJoke("one", "one one's loner two threeone one. "));
        }
    }
}