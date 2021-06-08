using Abp.AspNetCore;
using Abp.AspNetCore.TestBase;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HealthApp.EntityFrameworkCore;
using HealthApp.Web.Startup;
using Microsoft.AspNetCore.Mvc.ApplicationParts;

namespace HealthApp.Web.Tests
{
    [DependsOn(
        typeof(HealthAppWebMvcModule),
        typeof(AbpAspNetCoreTestBaseModule)
    )]
    public class HealthAppWebTestModule : AbpModule
    {
        public HealthAppWebTestModule(HealthAppEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbContextRegistration = true;
        } 
        
        public override void PreInitialize()
        {
            Configuration.UnitOfWork.IsTransactional = false; //EF Core InMemory DB does not support transactions.
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(HealthAppWebTestModule).GetAssembly());
        }
        
        public override void PostInitialize()
        {
            IocManager.Resolve<ApplicationPartManager>()
                .AddApplicationPartsIfNotAddedBefore(typeof(HealthAppWebMvcModule).Assembly);
        }
    }
}