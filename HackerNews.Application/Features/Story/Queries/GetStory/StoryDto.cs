using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Application.Features.Story.Queries.GetStoryDetails
{
    public class StoryDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;
    }
}
