using HackerNews.Application.Contracts;
using System.Net.Http;
using HackerNews.Domain;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using AutoMapper.Features;
using Polly.Registry;
using Polly;
using System.Drawing;

namespace HackerNews.Infrastructure
{
    public class HackerNewsClient : IHackerNewsClient
    {
        private readonly HttpClient _httpClient;
        private readonly IAsyncPolicy<Story> _storyCachePolicy;

        public HackerNewsClient(HttpClient httpClient, IConfiguration config, IReadOnlyPolicyRegistry<string> policyRegistry)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(config["HackerNewsBaseAddress"] ?? string.Empty);
            _storyCachePolicy = policyRegistry.Get<IAsyncPolicy<Story>>(nameof(Story));
        }

        public async Task<List<int>> GetNewStoryIds()
        {
            var response = await _httpClient.GetAsync("v0/newstories.json");
            var content = await response.Content.ReadAsStringAsync();
            var storyIds = JsonConvert.DeserializeObject<List<int>>(content);
            if (storyIds != null)
            {
                return storyIds;
            }

            return new List<int>();
        }

        public async Task<List<int>> GetNewStoryIdsByPage(int page, int size)
        {
            var response = await _httpClient.GetAsync("v0/newstories.json");
            var content = await response.Content.ReadAsStringAsync();
            var storyIds = JsonConvert.DeserializeObject<List<int>>(content);
            if (storyIds != null)
            {
                int startIndex = (page - 1) * size;
                List<int> paginatedStories = storyIds.Skip(startIndex).Take(size).ToList();
                return paginatedStories;
            }

            return new List<int>();
        }

        public async Task<Story?> GetStoryById(int id)
        {
            var policyExecutionContext = new Context($"Story-{id}");

            var story = await _storyCachePolicy.ExecuteAsync(async _ =>
            {
                var response = await _httpClient.GetAsync($"v0/item/{id}.json");
                var content = await response.Content.ReadAsStringAsync();
                var story = JsonConvert.DeserializeObject<Story>(content);
                return story;
            }, policyExecutionContext);

            return story;
        }
    }
}