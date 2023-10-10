using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNews.Application.Features.Story.Queries.GetStory;

namespace HackerNews.Application.Features.Story.Queries.SearchStories
{
    public record SearchStoriesByTextQuery(string? text) : IRequest<List<StoryDto>>;
}
