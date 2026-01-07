using AAMPTpl.Domain.Localization.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Domain;
using Volo.Abp.Localization;
using Volo.Abp.Modularity;
using Volo.Abp.MultiTenancy;
using Volo.Abp.SettingManagement;
using Volo.Abp.Settings;
using Volo.Abp.Validation;
using Volo.Abp.VirtualFileSystem;

namespace AAMPTpl.Domain
{
    [DependsOn(
        typeof(AbpDddDomainModule),
        typeof(AbpValidationModule),
        typeof(AbpSettingsModule),
        typeof(AbpSettingManagementDomainModule),
        typeof(AbpLocalizationModule),
        typeof(AbpVirtualFileSystemModule)
    )]
    public class AAMPTplDomainMobule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpVirtualFileSystemOptions>(options =>
            {
                // "YourRootNameSpace" is the root namespace of your project. It can be empty if your root namespace is empty.
                options.FileSets.AddEmbedded<AAMPTplDomainMobule>("AAMPTpl.Domain");
            });

            Configure<AbpLocalizationOptions>(options =>
            {
                //Define a new localization resource (WorkBoxResources)
                options.Resources
                    .Add<AAMPTplResources>("zh-Hans")
                    .AddVirtualJson("/Localization/Resources/AAMPTpl");
            });
        }
    }
}
