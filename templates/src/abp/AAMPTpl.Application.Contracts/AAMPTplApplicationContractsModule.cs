using AAMPTpl.Domain;
using System;
using Volo.Abp.Application;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace AAMPTpl.Application.Contracts
{
    [DependsOn(
        typeof(AbpDddApplicationContractsModule),
        typeof(AAMPTplDomainMobule),
        typeof(AbpSettingManagementApplicationContractsModule)
        )]
    public class AAMPTplApplicationContractsModule : AbpModule
    {
    }
}
