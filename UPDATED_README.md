# Name : Bobadilla, Raven Ray Marque
# Help Desk System - Prefinals Project - Output

An ASP.NET Core MVC app that connects to a pre-built SQLite database using Entity Framework Core.

## Technologies / Packages Used
- .NET 10.0
- ASP.NET Core MVC
- Entity Framework Core 10.0.11 
- Microsoft.EntityFramework.Core.Sqlite 10.0.11
- DB Browser for SQLite

## Database of the System
The database file is at `Data/lycevm.db`. It's already complete — this project only reads from it. No scaffolding, no migrations, no seeding.

## Features / What's Inside of the System?
- Browse departments, employees, teams, customers, and tickets
- View full ticket details (customer, category, priority, status, assignments, comments, tags, attachments)
- Create, update, and comment on tickets
- View reports: employee workload, department workload, unassigned tickets, tickets with multiple assignees, primary assignee, category hierarchy

## How to Run the System?
1. Open `IT_ELECTIVE_PREFINALS_PROJECT.slnx` in Visual Studio
2. Restore NuGet packages if prompted
3. Press `F5` to run
4. Open the local address that appears in your browser

See [DATABASE.md](DATABASE.md) for the database structure and table relationships.
