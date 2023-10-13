using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerNews.Application.Features.Story.Queries.GetStory
{
    public class StoryDto
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

        public int Score { get; set; }

        public DateTime Time { get; set; }

        public string By { get; set; } = string.Empty;
    }
}
