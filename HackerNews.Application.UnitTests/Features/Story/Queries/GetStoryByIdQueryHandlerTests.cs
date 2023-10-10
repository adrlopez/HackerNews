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

            var result = await handler.Handle(new GetStoryByIdQuery(37835465), CancellationToken.None);

            result.ShouldBeOfType<StoryDto>();
            result.Title.ShouldBe("Caroline Ellison took almost 30 seconds to recognize ex-boyfriend SBF");
            result.Url.ShouldBe("https://www.cnbc.com/2023/10/10/caroline-ellison-took-almost-30-seconds-to-recognize-ex-boyfriend-sbf.html");

            result = await handler.Handle(new GetStoryByIdQuery(37835456), CancellationToken.None);

            result.ShouldBeOfType<StoryDto>();
            result.Title.ShouldBe("The hubris of building AGI revealed");
            result.Url.ShouldBe("https://www.mindprison.cc/p/the-hubris-of-building-agi-revealed");

        }
    }
}
