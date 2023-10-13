using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HackerNews.Application.Contracts;
using HackerNews.Application.Features.Story.Queries.GetStory;
using HackerNews.Application.Models;
using HackerNews.Domain;
using MediatR;
using Microsoft.VisualBasic;

namespace HackerNews.Application.Features.Story.Queries.GetNewStories
{
    public class GetNewStoriesQueryHandler : IRequestHandler<GetNewStoriesQuery, PaginatedResponse<StoryDto>>
    {
        private readonly IHackerNewsClient _client;
        private readonly IMapper _mapper;

        public GetNewStoriesQueryHandler(IHackerNewsClient client, IMapper mapper)
        {
            this._mapper = mapper;
            this._client = client;
            
        }
        public async Task<PaginatedResponse<StoryDto>> Handle(GetNewStoriesQuery request, CancellationToken cancellationToken)
        {
            var tasks = new List<Task<Domain.Story?>>();

            async Task<Domain.Story?> GetStoryByIdLocal(int id)
            {
                var story = await _client.GetStoryById(id);
                return story;
            }

            var ids = await _client.GetNewStoryIds();

            foreach (var id in ids)
            {
                tasks.Add(GetStoryByIdLocal(id));
            }

            var stories = await Task.WhenAll(tasks);

            // filter stories

            var filteredStories = stories.AsQueryable();

            if (request?.title != null)
            {
                filteredStories = filteredStories.Where(s => s.Title.Contains(request.title, StringComparison.InvariantCultureIgnoreCase));
            }

            if (request?.createdBy != null)
            {
                filteredStories = filteredStories.Where(s => s.By.Contains(request.createdBy, StringComparison.InvariantCultureIgnoreCase));
            }

            if (request?.minScore != null)
            {
                filteredStories = filteredStories.Where(s => s.Score >= request.minScore);
            }

            if (request?.maxScore != null)
            {
                filteredStories = filteredStories.Where(s => s.Score <= request.maxScore);
            }

            if (request?.fromDt != null)
            {
                filteredStories = filteredStories.Where(s => s.Time >= request.fromDt);
            }

            if (request?.toDt != null)
            {
                filteredStories = filteredStories.Where(s => s.Time <= request.toDt);
            }

            return PaginatedResponse<StoryDto>.Create(filteredStories.Select(s => _mapper.Map<StoryDto>(s)),
                    request.page, request.size);
        }
    }
}
