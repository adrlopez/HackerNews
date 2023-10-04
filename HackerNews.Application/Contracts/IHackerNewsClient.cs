using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNews.Domain;

namespace HackerNews.Application.Contracts
{
    public interface IHackerNewsClient
    {
        Task<List<int>> GetNewStories();

        Task<Story> GetStoryById(int id);
    }
}
