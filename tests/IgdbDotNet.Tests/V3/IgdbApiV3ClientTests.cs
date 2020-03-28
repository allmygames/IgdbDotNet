using IgdbDotNet.V3;
using IgdbDotNet.V3.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IgdbDotNet.Tests.V3
{
    [TestClass]
    public class IgdbApiV3ClientTests
    {
        private const int MaxResponseCount = 200;

        private IgdbApiV3Client api;

        [TestInitialize]
        public void TestInitialize()
        {
            string apiKey = Environment.GetEnvironmentVariable("IGDB_API_KEY");
            Assert.IsNotNull(apiKey, "Could not load IGDB api key from IGDB_API_KEY environment variable.");

            api = new IgdbApiV3Client(apiKey);
        }

        [TestMethod]
        public async Task GetAllGames()
        {
            var response = await api.GetAllAsync<Game>();
            Assert.IsTrue(response.Count() > 0, "List should not be empty.");
            Assert.IsTrue(response.Count() <= MaxResponseCount, "List should not exceed max response count.");
            Assert.IsTrue(response.All(x => !string.IsNullOrEmpty(x.Name)),
                "All response values should have a non-empty name.");
            Assert.IsTrue(response.All(x => !string.IsNullOrEmpty(x.Slug)),
                "All response values should have a non-empty slug.");
        }

        [TestMethod]
        public async Task GetAllGenres()
        {
            var response = await api.GetAllAsync<Genre>();
            Assert.IsTrue(response.Count() > 0, "List should not be empty.");
            Assert.IsTrue(response.Count() <= MaxResponseCount, "List should not exceed max response count.");
            Assert.IsTrue(response.All(x => !string.IsNullOrEmpty(x.Name)),
                "All response values should have a non-empty name.");
            Assert.IsTrue(response.All(x => !string.IsNullOrEmpty(x.Slug)),
                "All response values should have a non-empty slug.");
        }

        [TestMethod]
        public async Task GetAllPlatforms()
        {
            var response = await api.GetAllAsync<Platform>();
            Assert.IsTrue(response.Count() > 0, "List should not be empty.");
            Assert.IsTrue(response.Count() <= MaxResponseCount, "List should not exceed max response count.");
            Assert.IsTrue(response.All(x => !string.IsNullOrEmpty(x.Name)),
                "All response values should have a non-empty name.");
            Assert.IsTrue(response.All(x => !string.IsNullOrEmpty(x.Slug)),
                "All response values should have a non-empty slug.");
        }

        [TestMethod]
        public async Task GetAllPulseSources()
        {
            var response = await api.GetAllAsync<PulseSource>();
            Assert.IsTrue(response.Count() > 0, "List should not be empty.");
            Assert.IsTrue(response.Count() <= MaxResponseCount, "List should not exceed max response count.");
            Assert.IsTrue(response.All(x => !string.IsNullOrEmpty(x.Name)));
        }

        [TestMethod]
        public async Task GetGameById()
        {
            const int GameId = 6;
            var response = await api.GetManyByIdAsync<Game>(new List<int>() { GameId });

            const string ExpectedGameName = "Baldur's Gate II: Shadows of Amn";
            Assert.AreEqual(ExpectedGameName, response.FirstOrDefault()?.Name);
        }

        [TestMethod]
        public async Task GetGenreById()
        {
            const int GenreId = 10;
            var response = await api.GetManyByIdAsync<Genre>(new List<int>() { GenreId });

            const string ExpectedGenreName = "Racing";
            Assert.AreEqual(ExpectedGenreName, response.FirstOrDefault()?.Name);

            const string ExpectedGenreSlug = "racing";
            Assert.AreEqual(ExpectedGenreSlug, response.FirstOrDefault()?.Slug);
        }

        [TestMethod]
        public async Task GetPlatformById()
        {
            const int PlatformId = 12;
            var response = await api.GetManyByIdAsync<Platform>(new List<int>() { PlatformId });

            const string ExpectedPlatformName = "Xbox 360";
            Assert.AreEqual(ExpectedPlatformName, response.FirstOrDefault()?.Name);

            const string ExpectedPlatformSlug = "xbox360";
            Assert.AreEqual(ExpectedPlatformSlug, response.FirstOrDefault()?.Slug);
        }
    }
}