using AAMPTpl.EntityFramework;
using AAMPTpl.EntityFramework.MiddleWares;
using AAMPTpl.ViewModels;
using AAMPTpl.Views;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging;
using Avalonia.Markup.Xaml;
using Avalonia.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp;

namespace AAMPTpl;

public partial class App : Avalonia.Application
{
    private IAbpApplicationWithInternalServiceProvider? _abpApplication;

    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    public static Action<IServiceCollection>? ConfigurePlatformServices { get; set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override async void OnFrameworkInitializationCompleted()
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
                .MinimumLevel.Debug()
#else
                .MinimumLevel.Information()
#endif
                .MinimumLevel.Override("Microsoft", (Serilog.Events.LogEventLevel)LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Async(c => c.File($"Logs/logs_{DateTime.Now.ToString("yyyyMMdd")}.txt"))
                .CreateLogger();

        try
        {
            // 根据平台确定可写的数据库目录，并在 ABP 初始化前注入连接字符串
            var dbDir = GetDatabaseDirectory();
            if (!Directory.Exists(dbDir))
                Directory.CreateDirectory(dbDir);
            var dbConnStr = $"Data Source={Path.Combine(dbDir, "CFUSV2.db")}";

            _abpApplication = await AbpApplicationFactory.CreateAsync<AAMPTplModule>(options =>
            {
                options.UseAutofac();
                options.Services.ReplaceConfiguration(
                    new ConfigurationBuilder()
                        .SetBasePath(AppContext.BaseDirectory)
                        .AddJsonFile("appsettings.json", optional: true)
                        .AddInMemoryCollection(new Dictionary<string, string?>
                        {
                            ["ConnectionStrings:Default"] = dbConnStr
                        })
                        .Build()
                );

                // 注册平台特定服务
                ConfigurePlatformServices?.Invoke(options.Services);
            });
            await _abpApplication.InitializeAsync();
            ServiceProvider = _abpApplication.ServiceProvider;

            // 自动迁移数据库
            ServiceProvider.UseAutoMigration<AAMPTplDbContext>();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "应用初始化失败");
            throw;
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is IActivityApplicationLifetime activityLifetime)
        {
            activityLifetime.MainViewFactory = () => new MainView
            {
                DataContext = new MainViewModel()
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = new MainViewModel()
            };
        }
        base.OnFrameworkInitializationCompleted();
    }

    private static string GetDatabaseDirectory()
    {
        // Android 上 SpecialFolder.Personal 对应应用私有可写目录
        if (OperatingSystem.IsAndroid())
            return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "db");
        return Path.Combine(AppContext.BaseDirectory, "db");
    }
}