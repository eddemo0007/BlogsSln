# BolgApp

Posts application developed with .NET Core v3., ASP.NET MVC, ASP.NET Identity Core, C# (VS 2019 Community Edition), Entity Framework Core and SQL Server.

## Installation

Download Source code.

### Generate password for Editor and Writer users

Application has already defined users for Writer and Editor roles (**writer@blogapp.com** and **editor@blogapp.com**).

Password must be store in the User Secrets of the project. To generate password run the following command:

```bash
dotnet user-secrets set "testUserPwd" <your-pwd>
```

These password will be used for predefined users access.

### Change the Database ConnectionString

In the **appsettings.json** file, change the **Data Source** and **Initial Catalog** values of the **DefaultConnetion** entry.

**ConnectionString** is refferencing a SQL Server Express database by default.

Database and seed data will be created when the application is running at first time.