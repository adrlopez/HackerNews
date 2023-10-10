using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HackerNews.Application.Contracts;
using HackerNews.Application.Features.Story.Queries.GetStory;
using HackerNews.Domain;
using MediatR;

namespace HackerNews.Application.Features.Story.Queries.GetNewStories
{
    public class SearchStoriesByTextQueryHandler : IRequestHandler<GetNewStoriesByPageQuery, List<StoryDto>>
    {
        private readonly IHackerNewsClient _client;
        private readonly IMapper _mapper;

        public SearchStoriesByTextQueryHandler(IHackerNewsClient client, IMapper mapper)
        {
            this._mapper = mapper;
            this._client = client;
            
        }
        public async Task<List<StoryDto>> Handle(GetNewStoriesByPageQuery request, CancellationToken cancellationToken)
        {
            // call the http client to get the new stories

            List<Domain.Story> result = new List<Domain.Story>();
            var ids = await _client.GetNewStoryIdsByPage(request.page, request.size);

            await Parallel.ForEachAsync(ids, cancellationToken, async (id, ct) =>
            {
                await GetStoryByIdLocal(id);

            });

            async Task GetStoryByIdLocal(int id)
            {
                var story = await _client.GetStoryById(id);
                if (story != null)
                {
                    result.Add(story);
                }
            }

            return _mapper.Map<List<StoryDto>>(result);


        }
    }
}
