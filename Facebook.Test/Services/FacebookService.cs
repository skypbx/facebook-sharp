// Copyright (c) 2020 Jonathan Rainier / skyPBX LLC. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading.Tasks;
using System.Web;
using Facebook.Lib;
using Facebook.Lib.Attributes;
using Facebook.Lib.Enums;
using Facebook.Lib.Extensions;
using Facebook.Lib.Interface;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Facebook.Test.Services
{
    public class FacebookService : IFacebookService
    {
        public FacebookService(ILogger<FacebookService> logger, IConfiguration config)
        {
            Controller = new FacebookController(config.GetValue<string>("Facebook:AccessToken"), ApiVersion.V8);

            logger.LogInformation("Initialized Facebook Service.");
        }

        public FacebookController Controller { get; set; }

        public async Task<T> GetAsync<T>(Dictionary<string, string> parameters = default)
        {
            var api = (ApiAttribute) typeof(T).GetCustomAttributes()
                .FirstOrDefault(x => x.GetType() == typeof(ApiAttribute));

            if (api == null)
                return default;

            var query = HttpUtility.ParseQueryString(string.Empty);

            if (parameters != default)
                foreach (var (key, value) in parameters)
                    query[key] = value;

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = Controller.BaseUri;
                var response = await httpClient.GetAsync($"{api.Path}?{query}");

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }

            return default;
        }

        public async Task<T> PostAsync<T>(object content, Dictionary<string, string> urlParameters = default,
            Dictionary<string, string> queryParameters = default)
        {
            var api = (ApiAttribute) typeof(T).GetCustomAttributes()
                .FirstOrDefault(x => x.GetType() == typeof(ApiAttribute));

            if (api == null)
                return default;

            var query = HttpUtility.ParseQueryString(string.Empty);

            if (queryParameters != default)
                foreach (var (key, value) in queryParameters)
                    query[key] = value;

            if (urlParameters != default)
                foreach (var (key, value) in urlParameters)
                    api.Path = api.Path.Replace($"{{{key}}}", value);

            var jsonContent = content != null ? content.AsJson() : new StringContent("{}");

            using (var httpClient = new HttpClient())
            {
                httpClient.BaseAddress = Controller.BaseUri;
                var response = await httpClient.PostAsync($"{api.Path}?{query}", jsonContent);

                if (response.IsSuccessStatusCode)
                    return JsonConvert.DeserializeObject<T>(await response.Content.ReadAsStringAsync());
            }

            return default;
        }
    }
}