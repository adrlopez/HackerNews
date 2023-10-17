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
            const int cacheExpirationInMinutes = 500; // aprox 1 story is created per minute. So, a story will be on the list of 500 stories for 500 minutes.
                                                      
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
