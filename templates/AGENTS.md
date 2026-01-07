# AAMPTpl开发提示

## 项目结构

**前端 (Avalonia UI)**

|项目	|说明|
|---	|---|
|AAMPTpl|	核心共享项目，包含视图和视图模型|
|AAMPTpl.Desktop|	桌面端启动项目 (Windows/macOS/Linux)|
|AAMPTpl.Android|	Android 启动项目|
|AAMPTpl.iOS|	iOS 启动项目|
|AAMPTpl.Browser|	WebAssembly 浏览器端项目|

**后端 (ABP Framework)**

|项目	|说明|
|---	|---|
|AAMPTpl.Domain|	领域层 - 实体、领域服务|
|AAMPTpl.Application.Contracts|	应用层契约 - DTO、接口定义|
|AAMPTpl.Application|	应用层实现|
|AAMPTpl.EntityFramework|	EF Core 基础设施|
|AAMPTpl.EntityFramework.MySql|	MySQL 支持|
|AAMPTpl.EntityFramework.SqlServer|	SQL Server 支持|

**目录结构**
```
AAMPTpl/                                    # 根目录
├── AGENTS.md                               # AI代理README
├── doc/                                    # 文档目录
└── src/                                    # 源代码目录
    ├── Directory.Packages.props            # NuGet 包版本集中管理    │
    ├── AAMPTpl/                            # 核心共享项目 (Avalonia UI)    
    ├── AAMPTpl.Desktop/                    # 桌面端启动项目 (Windows/macOS/Linux)    
    ├── AAMPTpl.Android/                    # Android 启动项目    
    ├── AAMPTpl.iOS/                        # iOS 启动项目    
    ├── AAMPTpl.Browser/                    # Web/Browser 启动项目 (WebAssembly)    
    └── abp/                                # ABP 框架后端模块
        ├── AAMPTpl.Domain/                 # 领域层
        │   ├── ApiResults/                 # API 返回结果定义
        │   ├── Config/                     # 配置
        │   ├── Extensions/                 # 扩展方法
        │   ├── Domains/                    # 领域实体
        │   └── Localization/               # 本地化资源        │
        ├── AAMPTpl.Application.Contracts/  # 应用层契约 (DTO、接口)        
        ├── AAMPTpl.Application/            # 应用层实现        
        ├── AAMPTpl.EntityFramework/        # EF Core 基础设施        
        │   ├── AAMPTplDbContext.cs        
        │   ├── Migrations/                 # 数据库迁移
        ├── AAMPTpl.EntityFramework.MySql/      # MySQL 数据库支持        │
        └── AAMPTpl.EntityFramework.SqlServer/  # SQL Server 数据库支持
```

## 前端开发提示

## 后端开发提示

- 依赖注入: [`doc\ABPRules\di.mdc`](./doc/ABPRules/di.mdc)
- 创建实体类: [`doc\ABPRules\task-create-entity.mdc`](./doc/ABPRules/task-create-entity.mdc)
- 创建Crud应用服务: [`doc\ABPRules\task-create-crud-service.mdc`](./doc/ABPRules/task-create-crud-service.mdc)
- 数据库迁移：[`doc\ABPRules\task-ef.mdc`](./doc/ABPRules/task-ef.mdc)
- 保存/读取配置: [`doc\ABPRules\task-save-read-setting.mdc`](./doc/ABPRules/task-save-read-setting.mdc)