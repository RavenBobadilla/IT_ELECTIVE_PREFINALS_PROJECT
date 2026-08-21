# Help Desk System

This project is a simple support-ticket web application made with ASP.NET Core MVC and SQLite.

## What it can do

- Show a dashboard with ticket totals.
- List all tickets, with search and status filtering.
- Show each ticket's full details, comments, and assigned staff.
- Create new tickets.
- Edit ticket details and status.
- Add comments to a ticket.

## Run the project

1. Open `IT_ELECTIVE_PREFINALS_PROJECT.slnx` in Visual Studio.
2. Press the green Run button or press `F5`.
3. The browser opens to the dashboard.

The database file is already included at `Data/lycevm.db`. Do not delete this file, because it contains the sample data used by the application.

## Main folders

- `Controllers` — receives requests and decides what page to show.
- `Models` — represents tickets, customers, status, and other data.
- `Data` — connects the project to the SQLite database.
- `Views` — the pages shown in the browser.
- `wwwroot` — CSS and other files used for the page design.
