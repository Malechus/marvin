
using System;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using marvin.Models;
using marvin.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace marvin
{
    public class Startup
    {
        public IConfigurationRoot Configuration { get; }

        private readonly string runtimeEnvironment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        public Startup(string[] args)
        {
            if (runtimeEnvironment is null)
            {
                runtimeEnvironment = "Development";
            }

            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .AddJsonFile($"appsettings.{runtimeEnvironment}.json");
            Configuration = builder.Build();
        }

        public static async Task RunAsync(string[] args)
        {
            var startup = new Startup(args);
            await startup.RunAsync();
        }

        public async Task RunAsync()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);

            var provider = services.BuildServiceProvider();
            provider.GetRequiredService<LoggerService>();
            provider.GetRequiredService<CommandHandler>();

            CommandCalls.Init(provider.GetRequiredService<CommandService>(), provider.GetRequiredService<HttpRequestHandler>());

            await provider.GetRequiredService<StartupService>().StartConnectionAsync();
            await Task.Delay(-1);
        }

        private void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Configuration)
            .AddSingleton(new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Verbose,
                MessageCacheSize = 1000,
                GatewayIntents = GatewayIntents.AllUnprivileged | GatewayIntents.MessageContent
            }))
            .AddSingleton(new CommandService(new CommandServiceConfig
            {
                LogLevel = LogSeverity.Verbose,
                DefaultRunMode = RunMode.Async
            }))
            .AddSingleton<CommandHandler>()
            .AddSingleton<StartupService>()
            .AddSingleton<LoggerService>()
            .AddSingleton<Random>()
            .AddSingleton<HttpRequestHandler>()
            .AddSingleton<DbService>();
        }
    }
}