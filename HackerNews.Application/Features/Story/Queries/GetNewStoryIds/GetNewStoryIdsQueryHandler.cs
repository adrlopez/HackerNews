using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNews.Application.Contracts;
using MediatR;

namespace HackerNews.Application.Features.Story.Queries.GetNewStoryIds
{
    public class GetNewStoriesQueryHandler : IRequestHandler<GetNewStoryIdsQuery, List<int>>
    {
        private readonly IHackerNewsClient _client;

        public GetNewStoriesQueryHandler(IHackerNewsClient client)
        {
            this._client = client;
            
        }
        public async Task<List<int>> Handle(GetNewStoryIdsQuery request, CancellationToken cancellationToken)
        {
            // call the http client to get the new stories

            var newStories = await _client.GetNewStoryIds(request.page, request.size);

            return newStories;

        }
    }
}
