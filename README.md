# Help Desk System

An ASP.NET Core MVC Help Desk application that manually maps and reads the provided SQLite database through Entity Framework Core.

## Technology

- .NET 10.0
- ASP.NET Core MVC
- Entity Framework Core 10.0.11
- Microsoft.EntityFrameworkCore.Sqlite 10.0.11
- SQLite

## Database

The existing database is at `Data/lycevm.db`. It is the source of truth. This project does not use database scaffolding or migrations, and it does not create, seed, or modify the database.

## Features

- Browse departments, employees, teams, customers, and tickets.
- View complete ticket details: customer, category, priority, status, assignments, comments, tags, and attachment records.
- Create, update, and comment on tickets.
- Use the Reports page for employee workload, department workload, unassigned tickets, multiple-assignee tickets, primary-assignee results, and category hierarchy.

## Run locally

1. Open `IT_ELECTIVE_PREFINALS_PROJECT.slnx` in Visual Studio.
2. Restore NuGet packages if prompted.
3. Run with `F5` or the green Run button.
4. Open the shown local address in a browser.

See [DATABASE.md](DATABASE.md) for the manual database investigation and relationship mapping.
