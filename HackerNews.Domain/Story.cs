using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json.Converters;

namespace HackerNews.Domain
{
    public class Story
    {
        public string By { get; set; } = string.Empty;
        public int Descendants { get; set; }

        public int Id { get; set; }
        
        public int Score { get; set; }

        public DateTime Time { get; set; }
        public string Title { get; set; } = string.Empty;

        public string Type { get; set; } = string.Empty;

        public string Url { get; set; } = string.Empty;

    }
}
