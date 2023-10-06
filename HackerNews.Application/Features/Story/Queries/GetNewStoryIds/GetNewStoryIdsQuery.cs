using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace HackerNews.Application.Features.Story.Queries.GetNewStoryIds
{
    public record GetNewStoryIdsQuery(int page, int size) : IRequest<List<int>>;
}