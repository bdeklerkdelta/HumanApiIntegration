using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using HealthApp.Configuration;
using HealthApp.Web;

namespace HealthApp.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class HealthAppDbContextFactory : IDesignTimeDbContextFactory<HealthAppDbContext>
    {
        public HealthAppDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<HealthAppDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            HealthAppDbContextConfigurer.Configure(builder, configuration.GetConnectionString(HealthAppConsts.ConnectionStringName));

            return new HealthAppDbContext(builder.Options);
        }
    }
}
