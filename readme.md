# PEP

**PEP** 是一个基于 ASP.NET Core 构建的 Web API 项目，主要用于管理用户、课程、帖子和相关的提交记录。项目采用了分层架构，利用控制器（Controllers）、领域模型（Models）、数据访问层（Repositories）以及 Entity Framework Core 来处理数据库交互。

## 项目结构

- **Controllers**  
  [PEP/Controllers/PostsController.cs](PEP/Controllers/PostsController.cs)  
  [PEP/Controllers/CoursesController.cs](PEP/Controllers/CoursesController.cs)  
  [PEP/Controllers/UsersController.cs](PEP/Controllers/UsersController.cs)  
  控制层负责接收客户端请求，调用相应的数据访问方法并返回 DTO 数据。例如：
  - `PostsController` 提供帖子、评论和回复的增删改查接口。
  - `CoursesController` 处理课程和子章节的管理接口（如添加课程、更新章节内容、删除课程等）。
  - `UsersController` 包含用户注册、提交、密码更新及课程列表操作。

- **Models**  
  [PEP/Models/Domain/User.cs](PEP/Models/Domain/User.cs)  
  [PEP/Models/Domain/Course.cs](PEP/Models/Domain/Course.cs)  
  [PEP/Models/Domain/Post.cs](PEP/Models/Domain/Post.cs)  
  项目定义了领域模型（例如 User、Course、Post、SubChapter 等），用于映射数据库中的实体。

- **Data**  
  [PEP/Data/FinalDesignContext.cs](PEP/Data/FinalDesignContext.cs)  
  数据库上下文继承自 `DbContext`，配置了实体间的关系和具体的数据库映射规则。

- **Repositories**  
  - 接口层：[PEP/Repositories/Interface/IPostRepository.cs](PEP/Repositories/Interface/IPostRepository.cs)  
    以及类似的课程和用户仓储接口
  - 实现层：[PEP/Repositories/Implement/ImpCourseRepository.cs](PEP/Repositories/Implement/ImpCourseRepository.cs)  
    [PEP/Repositories/Implement/ImpUserRepository.cs](PEP/Repositories/Implement/ImpUserRepository.cs)  
    [PEP/Repositories/Implement/ImpPostRepository.cs](PEP/Repositories/Implement/ImpPostRepository.cs)  
  采用了仓储模式封装数据访问细节，为各控制器提供必要的数据操作方法。

- **映射配置**  
  项目通过 AutoMapper 实现对象与 DTO 之间的映射，将领域模型转换为对外 API 使用的 DTO 格式。映射配置在 [PEP/Mappings/](PEP/Mappings/) 目录下（例如 AutoMapperProfiles）。

- **配置文件**  
  [PEP/appsettings.json](PEP/appsettings.json) 和 [PEP/appsettings.Development.json](PEP/appsettings.Development.json) 中配置了日志、连接字符串（数据库连接采用 SQL Server）等相关设置。

- **启动配置**  
  [PEP/Program.cs](PEP/Program.cs) 配置了服务容器，注册了 DbContext、各仓储和 AutoMapper，并启用了 Swagger 用于 API 文档。

## 接口功能说明

- **贴子相关接口**  
  由 [PEP/Controllers/PostsController.cs](PEP/Controllers/PostsController.cs) 提供。主要功能包括：  
  - **创建贴子**：使用 [`PEP.Models.DTO.Post.PostAddDTO`](PEP/Models/DTO/Post/PostAddDTO.cs) 定义新贴子的请求数据，并将数据传递给数据仓储层处理。  
  - **获取贴子**：提供查询所有贴子或单个贴子详情的接口。  
  - **更新贴子**：支持修改贴子标题、内容等信息。  
  - **删除贴子**：提供接口删除指定的贴子记录。

- **课程相关接口**  
  由 [PEP/Controllers/CoursesController.cs](PEP/Controllers/CoursesController.cs) 提供。主要功能包括：  
  - **管理课程**：创建、更新和删除课程信息；  
  - **管理子章节**：操作子章节信息，包括更新章节内容，其中 Markdown 内容的更新与 [`PEP.Models.DTO.Courses.Presentation.SubChapterMDContentDTO`](PEP/Models/DTO/Courses/Presentation/SubChapterMDContentDTO.cs) 定义的数据格式对应。

- **用户相关接口**  
  由 [PEP/Controllers/UsersController.cs](PEP/Controllers/UsersController.cs) 提供。主要功能包括：  
  - **用户注册与登录**：实现用户信息的录入及身份验证；  
  - **管理用户提交**：支持用户信息修改和其他关联操作，如查看用户的贴子记录等。

以上接口均通过各自对应的仓储（例如 [`PEP/Repositories/Implement/ImpPostRepository.cs`](PEP/Repositories/Implement/ImpPostRepository.cs)）层实现对数据库的操作，确保分层架构的清晰和代码的可维护性。

## 如何运行

1. 确保已安装 .NET 8 SDK 和 SQL Server 数据库。
2. 在项目根目录下执行以下命令编译项目：

   ```sh
   dotnet build
    ```

3. 启动项目：
   ```sh
   dotnet run --project PEP/PEP.csproj
    ```

## 总结
该项目展示了如何利用 ASP.NET Core 和 Entity Framework Core 构建一个结构清晰、可扩展的 Web API 应用。通过分层设计和仓储模式，项目实现了对用户、课程、帖子等业务逻辑的封装，并提供了丰富的 API 接口供客户端调用。


