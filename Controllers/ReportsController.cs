using IT_ELECTIVE_PREFINALS_PROJECT.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Controllers;

public class ReportsController(HelpDeskContext context) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.EmployeeWorkload = await context.Employees.Where(e => e.IsActive).Include(e => e.Department).Select(e => new { Employee = e.FullName, Department = e.Department.Name, Count = e.TicketAssignments.Count(a => a.UnassignedAt == null && !a.Ticket.Status.IsClosed) }).OrderBy(x => x.Employee).ToListAsync();
        ViewBag.DepartmentWorkload = await context.Departments.Select(d => new { Department = d.Name, EmployeeCount = d.Employees.Count, Count = d.Employees.SelectMany(e => e.TicketAssignments).Count(a => a.UnassignedAt == null && !a.Ticket.Status.IsClosed) }).OrderBy(x => x.Department).ToListAsync();
        ViewBag.Unassigned = await context.Tickets.Where(t => !t.Assignments.Any(a => a.UnassignedAt == null)).Include(t => t.Customer).Include(t => t.Priority).Include(t => t.Status).OrderByDescending(t => t.Id).ToListAsync();
        ViewBag.MultipleAssignees = await context.Tickets.Where(t => t.Assignments.Count(a => a.UnassignedAt == null) > 1).Include(t => t.Assignments.Where(a => a.UnassignedAt == null)).ThenInclude(a => a.Employee).ToListAsync();
        ViewBag.PrimaryAssignees = await context.Tickets.Include(t => t.Assignments.Where(a => a.UnassignedAt == null && a.IsPrimary)).ThenInclude(a => a.Employee).OrderBy(t => t.Id).ToListAsync();
        ViewBag.Categories = await context.TicketCategories.Include(c => c.ParentCategory).OrderBy(c => c.Name).ToListAsync();
        return View();
    }
}
