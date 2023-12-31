﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HackerNews.Application.Contracts;
using HackerNews.Application.Exceptions;
using MediatR;

namespace HackerNews.Application.Features.Story.Queries.GetStory
{
    public class GetStoryByIdQueryHandler : IRequestHandler<GetStoryByIdQuery, StoryDto>
    {
        private readonly IHackerNewsClient _client;
        private readonly IMapper _mapper;

        public GetStoryByIdQueryHandler(IMapper mapper, IHackerNewsClient client)
        {
            this._client = client;
            this._mapper = mapper;
            
        }
        public async Task<StoryDto> Handle(GetStoryByIdQuery request, CancellationToken cancellationToken)
        {
            // call the http client to get the story
            var story = await _client.GetStoryById(request.Id);

            // verify that story exists
            if (story == null || story.Type != "story")
                throw new NotFoundException(nameof(Story), request.Id);

            // convert entity to DTO object

            var storyDto = _mapper.Map<StoryDto>(story);

            return storyDto;

        }
    }
}
