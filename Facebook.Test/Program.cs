// Copyright (c) 2020 Jonathan Rainier / skyPBX LLC. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Facebook.Lib.Interface;
using Facebook.Lib.Models;
using Facebook.Lib.Models.Me;
using Facebook.Test.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Facebook.Test
{
    public static class Program
    {
        private static async Task Main()
        {
            var builder = new ConfigurationBuilder();
            BuildConfig(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Log.Logger.Information("Initializing...");

            var host = Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration((hostContext, configurationBuilder) =>
                {
                    var appAssembly =
                        Assembly.Load(new AssemblyName(hostContext.HostingEnvironment.ApplicationName));

                    configurationBuilder.AddUserSecrets(appAssembly, true);
                })
                .ConfigureServices((context, services) =>
                {
                    services.AddSingleton<IFacebookService, FacebookService>();
                })
                .UseSerilog()
                .Build();

            var configService = host.Services.GetService<IConfiguration>();
            var facebookService = ActivatorUtilities.CreateInstance<FacebookService>(host.Services);

            Log.Logger.Information("Finished initializing!");

            var accessToken = configService.GetValue<string>("Facebook:AccessToken");

            var accounts = await facebookService.GetAsync<Accounts>(new Dictionary<string, string>
            {
                {"access_token", accessToken}
            });

            Log.Logger.Information($"Posting to page {accounts.Data.Select(x => x.Name).FirstOrDefault()}");

            var pageAccessToken = accounts.Data.Select(x => x.AccessToken).FirstOrDefault();
            var pageId = accounts.Data.Select(x => x.Id).FirstOrDefault();

            var feed = await facebookService.PostAsync<Feed>(null,
                new Dictionary<string, string>
                {
                    {"page-id", pageId}
                },
                new Dictionary<string, string>
                {
                    {"access_token", pageAccessToken},
                    {"message", $"Test Post Created @ {DateTime.UtcNow:F}"}
                });

            Log.Logger.Information($"Successfully posted with ID {feed.Id}");

            await Task.Delay(Timeout.Infinite);
        }

        private static void BuildConfig(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables();
        }
    }
}