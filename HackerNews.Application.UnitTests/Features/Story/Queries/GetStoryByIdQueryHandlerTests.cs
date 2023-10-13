using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HackerNews.Application.Contracts;
using HackerNews.Application.Features.Story.Queries.GetStory;
using HackerNews.Application.MappingProfiles;
using HackerNews.Application.UnitTests.Mocks;
using Moq;
using Shouldly;

namespace HackerNews.Application.UnitTests.Features.Story.Queries
{
    public class GetStoryByIdQueryHandlerTests
    {
        private readonly Mock<IHackerNewsClient> _mockHackerNewsClient;
        private readonly IMapper _mapper;

        public GetStoryByIdQueryHandlerTests()
        {
            _mockHackerNewsClient = MockHackerNewsClient.GetMockHackerNewsClient();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<StoryProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetStoryByIdTest()
        {
            var handler = new GetStoryByIdQueryHandler(_mapper, _mockHackerNewsClient.Object);

            var result = await handler.Handle(new GetStoryByIdQuery(37874229), CancellationToken.None);

            result.ShouldBeOfType<StoryDto>();
            result.Title.ShouldBe("Tesla workers shared sensitive images recorded by customers' cars: Ex-employees");
            result.Url.ShouldBe("https://www.abc.net.au/news/2023-04-08/tesla-workers-shared-sensitive-images-recorded-by-customer-cars/102202382");

            result = await handler.Handle(new GetStoryByIdQuery(37874228), CancellationToken.None);

            result.ShouldBeOfType<StoryDto>();
            result.Title.ShouldBe("DigitalOcean Managed Kubernetes Postmortem");
            result.Url.ShouldBe("https://status.digitalocean.com/incidents/fsfsv9fj43w7");

        }
    }
}
