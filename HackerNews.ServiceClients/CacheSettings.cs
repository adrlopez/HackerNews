using Microsoft.Extensions.DependencyInjection;
using Polly.Caching;
using Polly;
using Polly.Registry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HackerNews.Domain;

namespace HackerNews.Infrastructure
{
    public static class CacheSettings
    {
        public static PolicyRegistry GetPolicyRegistry(IServiceProvider cacheProvider)
        {
            const int cacheExpirationInMinutes = 450; // made an estimate on how long a new story is kept in the list of new 500 stories.
            return new PolicyRegistry
            {
                {
                    nameof(Story),
                    Policy.CacheAsync(
                        cacheProvider
                            .GetRequiredService<IAsyncCacheProvider>()
                            .AsyncFor<Story>(),
                        TimeSpan.FromMinutes(cacheExpirationInMinutes))
                }
            };
        }
    }
}
