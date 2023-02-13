
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Prueba_Tecnica;

namespace Prueba_Tecnica.Model
{
    public class PruebaContext : DbContext
    {
        public PruebaContext() 
        { }
        public PruebaContext(DbContextOptions<PruebaContext> options) 
            : base(options) 
        { }

        public DbSet<Quizzez> quizzez { get; set; }
        public DbSet<Quiz_Questions> quiz_questions { get; set; }
        public DbSet<Questions> questions { get; set; }
        public DbSet<Answers> answers { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("AppDB"));
        }

    }
}
