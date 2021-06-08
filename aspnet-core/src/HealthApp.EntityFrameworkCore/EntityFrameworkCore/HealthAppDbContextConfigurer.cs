using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace HealthApp.EntityFrameworkCore
{
    public static class HealthAppDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<HealthAppDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<HealthAppDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
