using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNews.Application.Contracts;
using MediatR;

namespace HackerNews.Application.Features.Story.Queries.GetNewStories
{
    public class GetNewStoriesQueryHandler : IRequestHandler<GetNewStoriesQuery, List<int>>
    {
        private readonly IHackerNewsClient _client;

        public GetNewStoriesQueryHandler(IHackerNewsClient client)
        {
            this._client = client;
            
        }
        public async Task<List<int>> Handle(GetNewStoriesQuery request, CancellationToken cancellationToken)
        {
            // call the http client to get the new stories

            var newStories = await _client.GetNewStories();

            return newStories;

        }
    }
}
