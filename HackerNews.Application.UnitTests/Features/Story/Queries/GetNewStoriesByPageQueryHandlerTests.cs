using AutoMapper;
using HackerNews.Application.Contracts;
using HackerNews.Application.Features.Story.Queries.GetStory;
using HackerNews.Application.Features.Story.Queries.GetNewStories;
using HackerNews.Application.UnitTests.Mocks;
using HackerNews.Application.MappingProfiles;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNews.Application.Features.Story.Queries.GetNewStoryIds;
using Shouldly;

namespace HackerNews.Application.UnitTests.Features.Story.Queries
{
    public class GetNewStoriesByPageQueryHandlerTests
    {
        private readonly Mock<IHackerNewsClient> _mockHackerNewsClient;
        private readonly IMapper _mapper;
        
        public GetNewStoriesByPageQueryHandlerTests()
        {
            _mockHackerNewsClient = MockHackerNewsClient.GetMockHackerNewsClient();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<StoryProfile>();
            });

            _mapper = mapperConfig.CreateMapper();
        }

        [Fact]
        public async Task GetNewStoriesTest()
        {
            var handler = new GetNewStoriesByPageQueryHandler(_mockHackerNewsClient.Object, _mapper);

            var result = await handler.Handle(new GetNewStoriesByPageQuery(1,10), CancellationToken.None);

            result.ShouldBeOfType<List<StoryDto>>();
            result.Count.ShouldBe(4);
        }
    }
}
