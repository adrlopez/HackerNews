using System.Runtime.Serialization;
using HackerNews.Application.Features.Story.Queries.GetNewStories;
using HackerNews.Application.Features.Story.Queries.GetNewStoryIds;
using HackerNews.Application.Features.Story.Queries.GetStory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HackerNews.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StoriesController(IMediator mediator)
        {
            _mediator = mediator;
            
        }
        /// <summary>
        /// Get the new story ids
        /// </summary>
        /// <returns>A list of Ids</returns>
        [HttpGet]
        [Route("getNewStoryIds")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<int>>> GetNewStoryIds()
        {
            var newStoryIds = await _mediator.Send(new GetNewStoryIdsQuery());
            return Ok(newStoryIds);
        }

        /// <summary>
        /// Get the new stories
        /// </summary>
        /// <returns>A list of Stories</returns>
        [HttpGet]
        [Route("getNewStories")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<IEnumerable<StoryDto>>> GetNewStories([FromQuery] string? title, [FromQuery] int? minScore, [FromQuery] int? maxScore, [FromQuery] DateTime? fromDt, [FromQuery] DateTime? toDt, [FromQuery] string? createdBy, [FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var newStories = await _mediator.Send(new GetNewStoriesQuery(title, minScore, maxScore, fromDt, toDt, createdBy, page, size));
            return Ok(newStories);
        }

        /// <summary>
        /// Gets a story by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>The story</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<StoryDto>> Get(int id)
        {
            var story = await _mediator.Send(new GetStoryByIdQuery(id));
            return Ok(story);
        }
    }
}
