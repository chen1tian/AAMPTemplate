using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAMPTpl.EntityFramework
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AAMPTplDbContext>
    {
        public AAMPTplDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            var connString = configuration.GetConnectionString("Default");

            var optionsBuilder = new DbContextOptionsBuilder<AAMPTplDbContext>();
            optionsBuilder.UseSqlite(connString);
            return new AAMPTplDbContext(optionsBuilder.Options);
        }
    }
}
