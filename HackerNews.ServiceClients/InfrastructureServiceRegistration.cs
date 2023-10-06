using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using HackerNews.Application.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly.Caching;
using Polly.Registry;

namespace HackerNews.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddHttpClient<IHackerNewsClient, HackerNewsClient>();
            services.AddMemoryCache();
            services.AddSingleton<IAsyncCacheProvider, Polly.Caching.Memory.MemoryCacheProvider>();
            services.AddSingleton<IReadOnlyPolicyRegistry<string>, PolicyRegistry>(CacheSettings.GetPolicyRegistry);
            return services;
        }
    }
}
