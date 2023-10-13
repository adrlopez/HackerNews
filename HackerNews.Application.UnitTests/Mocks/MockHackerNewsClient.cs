using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNews.Application.Contracts;
using HackerNews.Domain;
using Moq;
using Newtonsoft.Json;

namespace HackerNews.Application.UnitTests.Mocks
{
    public class MockHackerNewsClient
    {
        public static Mock<IHackerNewsClient> GetMockHackerNewsClient()
        {
            var storyIds = new List<int>()
            {
                37874229, 37874228, 37874220, 37874193, 37874176
            };

            var stories = JsonConvert.DeserializeObject<List<Story>>(@"[
                {
                    ""id"": 37874229,
                    ""title"": ""Tesla workers shared sensitive images recorded by customers' cars: Ex-employees"",
                    ""url"": ""https://www.abc.net.au/news/2023-04-08/tesla-workers-shared-sensitive-images-recorded-by-customer-cars/102202382"",
                    ""score"": 1,
                    ""time"": ""2023-10-13T19:02:39Z"",
                    ""by"": ""samaysharma"",
                    ""type"": ""story""
                },
                {
                    ""id"": 37874228,
                    ""title"": ""DigitalOcean Managed Kubernetes Postmortem"",
                    ""url"": ""https://status.digitalocean.com/incidents/fsfsv9fj43w7"",
                    ""score"": 1,
                    ""time"": ""2023-10-13T19:02:38Z"",
                    ""by"": ""iudqnolq"",
                    ""type"": ""story""
                },
                {
                    ""id"": 37874220,
                    ""title"": ""Removal of Mazda Connected Services Integration"",
                    ""url"": ""https://www.home-assistant.io/blog/2023/10/13/removal-of-mazda-connected-services-integration/"",
                    ""score"": 1,
                    ""time"": ""2023-10-13T19:02:12Z"",
                    ""by"": ""andylynch"",
                    ""type"": ""story""
                },
                {
                    ""id"": 37874193,
                    ""title"": ""Rising Instability"",
                    ""url"": ""https://www.lynalden.com/october-2023-newsletter/"",
                    ""score"": 2,
                    ""time"": ""2023-10-13T19:00:05Z"",
                    ""by"": ""jger15"",
                    ""type"": ""story""
                },
                {
                    ""id"": 37874176,
                    ""title"": ""Ask HN: What is your favourite Indie Hackers episode?"",
                    ""url"": """",
                    ""score"": 1,
                    ""time"": ""2023-10-13T18:58:02Z"",
                    ""by"": ""kimchidude"",
                    ""type"": ""story""
                }
            ]");


            var mockHackerNewsClient = new Mock<IHackerNewsClient>();
            mockHackerNewsClient.Setup(c => c.GetNewStoryIds()).ReturnsAsync(storyIds);
            mockHackerNewsClient.Setup(c => c.GetStoryById(37874229)).ReturnsAsync(stories[0]);
            mockHackerNewsClient.Setup(c => c.GetStoryById(37874228)).ReturnsAsync(stories[1]);
            mockHackerNewsClient.Setup(c => c.GetStoryById(37874220)).ReturnsAsync(stories[2]);
            mockHackerNewsClient.Setup(c => c.GetStoryById(37874193)).ReturnsAsync(stories[3]);
            mockHackerNewsClient.Setup(c => c.GetStoryById(37874176)).ReturnsAsync(stories[4]);

            return mockHackerNewsClient;
        }
    }
}