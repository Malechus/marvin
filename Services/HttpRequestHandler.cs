using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using marvin.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NewsAPI;
using NewsAPI.Constants;
using NewsAPI.Models;

namespace marvin.Services
{
    public class HttpRequestHandler
    {
        private readonly IConfigurationRoot config;
        private readonly string newstoken;

        public HttpRequestHandler(IConfigurationRoot configurationRoot)
        {
            config = configurationRoot;
            newstoken = config.GetRequiredSection("Settings").Get<Settings>().NewsKey;
        }

        #region News Requests
        public List<Article> GetArticles()
        {
            List<Article> result = new List<Article>();

            NewsApiClient client = new NewsApiClient(newstoken);
            var response = client.GetTopHeadlines(new TopHeadlinesRequest
            {
                PageSize = 5,
                Page = 1,
                Country = Countries.US
            });

            if (response.Status == Statuses.Ok)
            {
                foreach (Article a in response.Articles)
                {
                    result.Add(a);
                }
            }
            else
            {
                return null;
            }

            return result;
        }
        #endregion
    }
}