# Database Investigation

Database file: `Data/lycevm.db`. It was inspected manually and used as-is. No migrations, scaffolding, table creation, seed data, or database changes were used.

| Table | Primary key | Notes |
|---|---|---|
| Departments | Id | Has many Employees and Teams. |
| Employees | Id | Required DepartmentId; has many assignments, comments, and team memberships. |
| Teams | Id | Required DepartmentId; has many TeamMembers. |
| TeamMembers | TeamId, EmployeeId | Composite key joining Teams and Employees; JoinedAt is required. |
| Customers | Id | Has many Tickets. Phone is nullable. |
| Tickets | Id | Required customer, category, priority, and status; DueAt, ResolvedAt, and ClosedAt are nullable. |
| TicketStatuses | Id | Has many Tickets. |
| TicketPriorities | Id | Has many Tickets. |
| TicketCategories | Id | ParentCategoryId is nullable and references TicketCategories.Id. |
| TicketAssignments | TicketId, EmployeeId | Composite key; links Tickets and Employees. UnassignedAt is nullable. |
| TicketComments | Id | Required TicketId; EmployeeId is optional. |
| Tags | Id | Connected to Tickets through TicketTags. |
| TicketTags | TicketId, TagId | Composite key many-to-many join. |
| TicketAttachments | Id | Required TicketId; records attachment metadata only. |

## Relationships

- One Department to many Employees and Teams.
- One Team to many Employees through TeamMembers (many-to-many).
- One Customer, Category, Priority, and Status to many Tickets.
- One Ticket to many Assignments, Comments, Attachments, and TicketTags.
- Employees and Tickets have a many-to-many relationship through TicketAssignments.
- Tickets and Tags have a many-to-many relationship through TicketTags.
- Categories have an optional self-reference: a category can have one parent and many subcategories. Root categories have no parent.

Nullable database columns are represented by nullable C# properties, especially `Phone`, `ParentCategoryId`, `DueAt`, `ResolvedAt`, `ClosedAt`, `UnassignedAt`, and `EmployeeId` on comments.
