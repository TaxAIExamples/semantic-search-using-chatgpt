using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Configuration;
using TrainingDataChatGPTApp;
using Microsoft.EntityFrameworkCore;
using System.Windows.Forms;
using ChatGPTInterface;

namespace SemanticSearch
{
    internal static class Program
    {
        private static IServiceProvider serviceProvider;

        [STAThread]
        static void Main()
        {
            // Set up dependency injection
            ConfigureServices();

            using (IServiceScope scope = serviceProvider.CreateScope())
            {
                IServiceProvider services = scope.ServiceProvider;
                MainSearchForm mainForm = services.GetRequiredService<MainSearchForm>();
                Application.Run(mainForm);
            }
        }
            
        private static void ConfigureServices()
        {
            IConfigurationBuilder configBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfiguration configuration = configBuilder.Build();

            // Set up the services and the DI container
            ServiceCollection services = new ServiceCollection();

            // Register the configuration
            services.AddSingleton<IConfiguration>(configuration);

            // Register the logging services
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
            });

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(configuration.GetSection("Logging"));
                loggingBuilder.AddConsole();
            });

            // Register the Entity Framework DbContext
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            // Register your services and dependencies here
            services.AddTransient<MainSearchForm>();
            services.AddScoped<IChatGPT, ChatGPT>();
            services.AddScoped<IKnowledgeRecordManager, KnowledgeRecordManager>();

            serviceProvider = services.BuildServiceProvider();
        }

    }
}