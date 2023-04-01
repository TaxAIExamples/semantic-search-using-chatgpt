using Microsoft.Extensions.Configuration;
using System.Configuration;
using TrainingDataChatGPTApp;

namespace SemanticSearch
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var configuration = BuildConfiguration();
            
            ApplicationConfiguration.Initialize();
            Application.Run(new MainSearchForm(configuration));
        }

        private static IConfiguration BuildConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            return builder.Build();
        }

    }
}