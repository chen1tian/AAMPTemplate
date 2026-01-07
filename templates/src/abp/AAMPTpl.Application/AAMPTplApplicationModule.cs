using AAMPTpl.Application.Contracts;
using AAMPTpl.Domain;
using AAMPTpl.EntityFramework;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using Volo.Abp.Application;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace AAMPTpl.Application
{
    [DependsOn(
        typeof(AAMPTplDomainMobule),
        typeof(AAMPTplEntityFrameworkModule),
        typeof(AAMPTplApplicationContractsModule),
        typeof(AbpAutoMapperModule),
        typeof(AbpDddApplicationModule),
        typeof(AbpSettingManagementApplicationModule)
        )]
    public class AAMPTplApplicationModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            
        }
    }
}
