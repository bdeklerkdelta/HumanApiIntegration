using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using HealthApp.Authorization;

namespace HealthApp
{
    [DependsOn(
        typeof(HealthAppCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class HealthAppApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<HealthAppAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(HealthAppApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddMaps(thisAssembly)
            );
        }
    }
}
