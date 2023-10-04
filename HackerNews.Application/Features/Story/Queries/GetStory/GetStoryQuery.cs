using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNews.Application.Features.Story.Queries.GetStoryDetails;
using MediatR;

namespace HackerNews.Application.Features.Story.Queries.GetStory;
public record GetStoryQuery(int Id) : IRequest<StoryDto>;
 

