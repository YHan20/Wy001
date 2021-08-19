># WebApi 项目
>>## 工具 Visual Studio Code (vs code) 
```sh
基于`asp.net core`和`netcoreapp3.1`，

下载路径：https://docs.microsoft.com/zh-cn/dotnet/core/sdk
```

>### 1. 使用Restfull约定，来分别完成数据的CRUD，类似如下用法：

    | 路由形式 | 说明 |
    | - | -|
    |    get /users  | 获取用户列表 |
    |    get /users/:id     |  获取指定id的用户 |
    |    post /users | 增加用户 |
    |    put /users/:id | 修改指定用户 |
    |    delete /users/:id | 删除指定用户 |

>### 2. VSCode 的应用 及 各种（在当前开发环境下）插件（C#、C# Extension、C# XML Document 、Rest Client）的使用

>## 3. dotnet命令的各种应用 (如：文件名为 WebApi )
```sh
dotnet new sln -n WebApi

cd WebApi services.msc

+ # 创建项目 WebApi.Api
dotnet new webapi -n WebApi.Api

dotnet sln add WebApi.Api

cd WebApi.Api

dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.SqlServer

(定义实体类型、数据库上下文，定义数据连接字符串)

dotnet tool install --global dotnet-ef

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add XXXX

dotnet ef database update
```

>## 还原项目
```sh
+ # 克隆项目下来先还原项目

dotnet restore
```

>## 要用的（经常的吧）
```sh
dotnet new sln

dotnet new webapi -n Lautism.Baseend.Api --no-https

dotnet add Lautism.Backend.Api

dotnet add package Microsoft.EntityFrameworkCore

dotnet add package Microsoft.EntityFrameworkCore.SqlServer

(定义实体类型、数据库上下文，定义数据连接字符串)

dotnet tool install --global dotnet-ef

dotnet add package Microsoft.EntityFrameworkCore.Design

dotnet ef migrations add XXXX

dotnet ef database update
````