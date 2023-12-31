﻿using AutoMapper;
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
using HackerNews.Application.Models;
using Shouldly;

namespace HackerNews.Application.UnitTests.Features.Story.Queries
{
    public class GetNewStoriesQueryHandlerTests
    {
        private readonly Mock<IHackerNewsClient> _mockHackerNewsClient;
        private readonly IMapper _mapper;
        
        public GetNewStoriesQueryHandlerTests()
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
            var handler = new GetNewStoriesQueryHandler(_mockHackerNewsClient.Object, _mapper);

            var result = await handler.Handle(new GetNewStoriesQuery(1,10), CancellationToken.None);

            result.ShouldBeOfType<PaginatedResponse<StoryDto>>();
            result.TotalItems.ShouldBe(5);
        }

        [Fact]
        public async Task GetFilteredStoriesTest()
        {
            var handler = new GetNewStoriesQueryHandler(_mockHackerNewsClient.Object, _mapper);

            var result = await handler.Handle(new GetNewStoriesQuery("Rising", 1, 5, null, null, "jger15", 1, 10), CancellationToken.None);

            result.ShouldBeOfType<PaginatedResponse<StoryDto>>();
            result.TotalItems.ShouldBe(1);
        }


        [Fact]
        public async Task GetFilteredStoriesNoResultsTest()
        {
            var handler = new GetNewStoriesQueryHandler(_mockHackerNewsClient.Object, _mapper);

            var result = await handler.Handle(new GetNewStoriesQuery("Rising", 1, 5, null, null, "alopez", 1, 10), CancellationToken.None);

            result.ShouldBeOfType<PaginatedResponse<StoryDto>>();
            result.TotalItems.ShouldBe(0);
        }
    }
}
