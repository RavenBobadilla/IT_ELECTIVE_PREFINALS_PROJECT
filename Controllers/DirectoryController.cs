using IT_ELECTIVE_PREFINALS_PROJECT.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Controllers;

public class DirectoryController(HelpDeskContext context) : Controller
{
    public async Task<IActionResult> Departments() => View(await context.Departments.Include(d => d.Employees).OrderBy(d => d.Name).ToListAsync());
    public async Task<IActionResult> Employees() => View(await context.Employees.Include(e => e.Department).OrderBy(e => e.LastName).ThenBy(e => e.FirstName).ToListAsync());
    public async Task<IActionResult> Teams() => View(await context.Teams.Include(t => t.Department).Include(t => t.TeamMembers).ThenInclude(m => m.Employee).OrderBy(t => t.Name).ToListAsync());
    public async Task<IActionResult> Customers() => View(await context.Customers.OrderBy(c => c.CompanyName).ToListAsync());
}
