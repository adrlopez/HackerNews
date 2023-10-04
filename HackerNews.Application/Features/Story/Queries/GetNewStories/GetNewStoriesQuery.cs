using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HackerNews.Application.Features.Story.Queries.GetNewStories
{
    public record GetNewStoriesQuery : IRequest<List<int>>;
}