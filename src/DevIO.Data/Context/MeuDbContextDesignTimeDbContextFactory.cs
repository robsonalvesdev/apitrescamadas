using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DevIO.Data.Context
{
    public class MeuDbContextDesignTimeDbContextFactory : IDesignTimeDbContextFactory<MeuDbContext>
    {
        public MeuDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<MeuDbContext>();

            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            builder
                .UseSqlServer(configuration.GetConnectionString("SqlServer"));

            return new MeuDbContext(builder.Options);
        }
    }
}
