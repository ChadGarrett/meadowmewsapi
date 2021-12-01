# MeadowMewsAPI

API for the dashboard that I use to help manage my home life.

## Features

### Current:
1. Tracking of monthly levy statements
2. Tracking of monthly water statements
3. Tracking of prepaid electricity purchases

### Planned:

- Statistic endpoints showing summaries, totals and averages of the tracked statements for a graph page.
- Turn the API into an entire estate management tool, by allowing multiple households to be added under one estate.



## Cheatsheet

### Run and build commands

> $ dotnet run

> $ dotnet build

> $ dotnet test

Live reload changes:
> $ dotnet watch run

### Controller scaffolding

> $ dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design

> $ dotnet add package Microsoft.EntityFrameworkCore.Design

> $ dotnet add package Microsoft.EntityFrameworkCore.SqlServer

> $ dotnet tool install -g dotnet-aspnet-codegenerator

> $ dotnet aspnet-codegenerator controller -name TodoItemsController -async -api -m TodoItem -dc TodoContext -outDir Controllers
