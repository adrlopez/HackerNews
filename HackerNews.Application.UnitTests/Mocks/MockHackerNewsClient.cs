using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNews.Application.Contracts;
using HackerNews.Domain;
using Moq;

namespace HackerNews.Application.UnitTests.Mocks
{
    public class MockHackerNewsClient
    {
        public static Mock<IHackerNewsClient> GetMockHackerNewsClient()
        {

            var storyIds = new List<int>()
            {
                37835465, 37835456, 37835397, 37835383
            };

            var stories = new List<Story>()
            {
                new Story()
                {
                    Id = 37835465,
                    Title = "Caroline Ellison took almost 30 seconds to recognize ex-boyfriend SBF",
                    Url = "https://www.cnbc.com/2023/10/10/caroline-ellison-took-almost-30-seconds-to-recognize-ex-boyfriend-sbf.html",
                    Type = "story"
                },
                new Story()
                {
                    Id = 37835456,
                    Title = "The hubris of building AGI revealed",
                    Url = "https://www.mindprison.cc/p/the-hubris-of-building-agi-revealed",
                    Type = "story"
                },
                new Story()
                {
                    Id = 37835397,
                    Title = "Trust Cafe",
                    Url = "https://www.trustcafe.io/en",
                    Type = "story"
                },
                new Story()
                {
                    Id = 37835383,
                    Title = "Airbnb listings down to 3,227 from 22,434 since August",
                    Url = "https://www.wired.com/story/airbnb-ban-new-york-illegal-listings/",
                    Type = "story"
                }
            };

            var mockHackerNewsClient = new Mock<IHackerNewsClient>();
            mockHackerNewsClient.Setup(c => c.GetNewStoryIds()).ReturnsAsync(storyIds);
            mockHackerNewsClient.Setup(c => c.GetNewStoryIdsByPage(1,10)).ReturnsAsync(storyIds);
            mockHackerNewsClient.Setup(c => c.GetStoryById(37835465)).ReturnsAsync(stories[0]);
            mockHackerNewsClient.Setup(c => c.GetStoryById(37835456)).ReturnsAsync(stories[1]);
            mockHackerNewsClient.Setup(c => c.GetStoryById(37835397)).ReturnsAsync(stories[2]);
            mockHackerNewsClient.Setup(c => c.GetStoryById(37835383)).ReturnsAsync(stories[3]);

            return mockHackerNewsClient;

        }
    }
}
