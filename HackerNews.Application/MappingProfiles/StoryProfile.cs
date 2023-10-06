using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HackerNews.Application.Features.Story.Queries.GetStory;
using HackerNews.Domain;

namespace HackerNews.Application.MappingProfiles
{
    public class StoryProfile : Profile
    {
        public StoryProfile()
        {
            CreateMap<Story, StoryDto>();
        }
    }
}
