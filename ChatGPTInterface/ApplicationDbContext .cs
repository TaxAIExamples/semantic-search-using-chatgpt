using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SemanticSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatGPTInterface
{
    public class ApplicationDbContext : DbContext
    {

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(
            DbContextOptions<ApplicationDbContext> options
            ) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                   .SetBasePath(Directory.GetCurrentDirectory())
                   .AddJsonFile("appsettings.json")
                   .Build();
                var connectionString = configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<KnowledgeRecord> KnowledgeRecords { get; set; }
        public DbSet<KnowledgeVectorItem> KnowledgeVectorItems { get; set; }
    }
}
