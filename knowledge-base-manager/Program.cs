using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TrainingDataChatGPTApp;
using Microsoft.Extensions.Logging;
using ChatGPTInterface;
using Microsoft.EntityFrameworkCore;

namespace KnowledgeBaseManager
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
                MainManageForm mainForm = services.GetRequiredService<MainManageForm>();
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
            services.AddTransient<MainManageForm>();
            services.AddScoped<IChatGPT, ChatGPT>();
            services.AddScoped<IKnowledgeRecordManager, KnowledgeRecordManager>();

            serviceProvider = services.BuildServiceProvider();
        }
    }
}