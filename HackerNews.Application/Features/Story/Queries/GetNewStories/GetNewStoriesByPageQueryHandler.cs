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
    public class GetNewStoriesByPageQueryHandler : IRequestHandler<GetNewStoriesByPageQuery, List<StoryDto>>
    {
        private readonly IHackerNewsClient _client;
        private readonly IMapper _mapper;

        public GetNewStoriesByPageQueryHandler(IHackerNewsClient client, IMapper mapper)
        {
            this._mapper = mapper;
            this._client = client;
            
        }
        public async Task<List<StoryDto>> Handle(GetNewStoriesByPageQuery request, CancellationToken cancellationToken)
        {
            var tasks = new List<Task<Domain.Story?>>();

            async Task<Domain.Story?> GetStoryByIdLocal(int id)
            {
                var story = await _client.GetStoryById(id);
                return story;
            }

            var ids = await _client.GetNewStoryIdsByPage(request.page, request.size);

            foreach (var id in ids)
            {
                tasks.Add(GetStoryByIdLocal(id));
            }

            var result = await Task.WhenAll(tasks);

            return _mapper.Map<List<StoryDto>>(result.Where(s => s != null));


        }
    }
}
