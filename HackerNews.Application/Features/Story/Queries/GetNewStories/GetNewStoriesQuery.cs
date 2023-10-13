using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNews.Application.Features.Story.Queries.GetStory;
using HackerNews.Application.Models;

namespace HackerNews.Application.Features.Story.Queries.GetNewStories
{
    public record GetNewStoriesQuery(string? title, int? minScore, int? maxScore, DateTime? fromDt, DateTime? toDt,
        string? createdBy, int page, int size) : IRequest<PaginatedResponse<StoryDto>>
    {
        public GetNewStoriesQuery(int page, int size) : this(null, null, null, null, null, null, page, size) { }
    };
}
