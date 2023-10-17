using System.Drawing;
using HackerNews.Application.Features.Story.Queries.GetNewStories;
using MediatR;

namespace HackerNews.Api
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IMediator _mediator;

        public Worker(ILogger<Worker> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                // get all the stories. Even though I select only page 1, the cache will be refreshed.
                var newStories = await _mediator.Send(new GetNewStoriesQuery(1,10), CancellationToken.None);
                await Task.Delay(60000, stoppingToken);
            }
        }
    }
}