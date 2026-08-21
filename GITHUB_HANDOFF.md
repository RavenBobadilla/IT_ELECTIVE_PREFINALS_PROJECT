# GitHub Handoff

The finished local project has four focused feature branches:

1. `feature/manual-ef-model` — manual EF Core entities, relationships, DbContext, SQLite connection.
2. `feature/ticket-management` — dashboard, ticket list, ticket details, create/edit forms, comments.
3. `feature/browse-helpdesk-records` — departments, employees, teams, customers, and navigation.
4. `feature/required-workload-reports` — all required workload queries, hierarchy report, `DATABASE.md`, and improved README.

These names are provisional because the required **Proposed Branch per User Story** document was not supplied with the available files. Rename branches only after comparing them with that document.

## Suggested PR order

Create and merge pull requests in the same order as the branches above. Each PR should have a short description, build result, and a review by the other project partner. Do not claim or create a review on behalf of another person; each partner needs to use their own GitHub account for their commits and review.

## Remote repository issue

The configured repository currently points to `RavenBobadilla/IT_ELECTIVE_PREFINALS_PROJECT`, but its available `main` reference contains a different `ConsoleApp1` project. Confirm the correct repository and authenticate GitHub in Visual Studio or Git Credential Manager before pushing these branches.
