using AAMPTpl.Application;
using AAMPTpl.EntityFramework;
using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.AutoMapper;
using Volo.Abp.Modularity;

namespace AAMPTpl
{
    public class AAMPTplModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            var configuration = context.Services.GetConfiguration();
            var services = context.Services;

            // 配置数据库
            context.Services.AddDbContext<AAMPTplDbContext>();

            // 配置AutoMapper
            ConfigureAutoMap(context.Services);
        }

        /// <summary>
        /// 配置AutoMapper
        /// </summary>
        /// <param name="services"></param>
        private void ConfigureAutoMap(IServiceCollection services)
        {
            // 对象映射
            services.AddAutoMapperObjectMapper<AAMPTplModule>();
            services.AddAutoMapperObjectMapper<AAMPTplApplicationModule>();

            AutoMapper.IConfigurationProvider config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AAMPTplAutoMapProfile>();
                cfg.AddProfile<AAMPTplApplicationAutoMapProfile>();
            });
            services.AddSingleton(config);
            services.AddScoped<IMapper, Mapper>();

            Configure<AbpAutoMapperOptions>(options =>
            {
                options.AddMaps<AAMPTplModule>(validate: true);
                options.AddMaps<AAMPTplApplicationModule>(validate: true);
            });
        }
    }
}
