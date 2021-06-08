using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using HealthApp.Authorization.Roles;
using HealthApp.Authorization.Users;
using HealthApp.MultiTenancy;

namespace HealthApp.EntityFrameworkCore
{
    public class HealthAppDbContext : AbpZeroDbContext<Tenant, Role, User, HealthAppDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public DbSet<HealthInfo.HealthInfo> HealthInfos { get; set; }

        public DbSet<HealthInfo.SourceHealthInfo> SourceHealthInfos { get; set; }

        public HealthAppDbContext(DbContextOptions<HealthAppDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<HealthInfo.HealthInfo>().ToTable("HealthInfos");
            modelBuilder.Entity<HealthInfo.SourceHealthInfo>().ToTable("SourceHealthInfos");
        }
    }
}