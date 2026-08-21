using IT_ELECTIVE_PREFINALS_PROJECT.Data;
using IT_ELECTIVE_PREFINALS_PROJECT.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Controllers;

public class TicketsController(HelpDeskContext context) : Controller
{
    public async Task<IActionResult> Index(string? search, int? statusId)
    {
        var tickets = context.Tickets.Include(t => t.Customer).Include(t => t.Category).Include(t => t.Priority).Include(t => t.Status).AsQueryable();
        if (!string.IsNullOrWhiteSpace(search)) tickets = tickets.Where(t => t.Subject.Contains(search) || t.Customer.CompanyName.Contains(search));
        if (statusId.HasValue) tickets = tickets.Where(t => t.StatusId == statusId);
        ViewBag.Search = search;
        ViewBag.StatusId = new SelectList(await context.TicketStatuses.OrderBy(s => s.Name).ToListAsync(), "Id", "Name", statusId);
        return View(await tickets.OrderByDescending(t => t.Id).ToListAsync());
    }

    public async Task<IActionResult> Details(int id)
    {
        var ticket = await context.Tickets.Include(t => t.Customer).Include(t => t.Category).Include(t => t.Priority).Include(t => t.Status).Include(t => t.TicketTags).ThenInclude(tt => tt.Tag).Include(t => t.Attachments).FirstOrDefaultAsync(t => t.Id == id);
        if (ticket is null) return NotFound();
        ViewBag.Comments = await context.TicketComments.Where(c => c.TicketId == id).Include(c => c.Employee).OrderByDescending(c => c.Id).ToListAsync();
        ViewBag.Assignments = await context.TicketAssignments.Where(a => a.TicketId == id && a.UnassignedAt == null).Include(a => a.Employee).ToListAsync();
        return View(ticket);
    }

    public async Task<IActionResult> Create() => View(await CreateForm());

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TicketFormViewModel form)
    {
        if (!ModelState.IsValid) return View(await CreateForm(form));
        var now = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        context.Tickets.Add(new Ticket { CustomerId = form.CustomerId, CategoryId = form.CategoryId, PriorityId = form.PriorityId, StatusId = form.StatusId, Subject = form.Subject, Description = form.Description, DueAt = form.DueAt, CreatedAt = now, UpdatedAt = now });
        await context.SaveChangesAsync();
        TempData["Message"] = "Ticket created successfully.";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var ticket = await context.Tickets.FindAsync(id);
        if (ticket is null) return NotFound();
        return View(await CreateForm(new TicketFormViewModel { Id = ticket.Id, CustomerId = ticket.CustomerId, CategoryId = ticket.CategoryId, PriorityId = ticket.PriorityId, StatusId = ticket.StatusId, Subject = ticket.Subject, Description = ticket.Description, DueAt = ticket.DueAt }));
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(TicketFormViewModel form)
    {
        var ticket = await context.Tickets.FindAsync(form.Id);
        if (ticket is null) return NotFound();
        if (!ModelState.IsValid) return View(await CreateForm(form));
        ticket.CustomerId = form.CustomerId; ticket.CategoryId = form.CategoryId; ticket.PriorityId = form.PriorityId; ticket.StatusId = form.StatusId; ticket.Subject = form.Subject; ticket.Description = form.Description; ticket.DueAt = form.DueAt; ticket.UpdatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        var status = await context.TicketStatuses.FindAsync(form.StatusId);
        if (status?.IsClosed == true && ticket.ClosedAt is null) ticket.ClosedAt = ticket.UpdatedAt;
        await context.SaveChangesAsync();
        TempData["Message"] = "Ticket updated successfully.";
        return RedirectToAction(nameof(Details), new { id = ticket.Id });
    }

    [HttpPost, ValidateAntiForgeryToken]
    public async Task<IActionResult> AddComment(int ticketId, string comment)
    {
        if (!string.IsNullOrWhiteSpace(comment)) { context.TicketComments.Add(new TicketComment { TicketId = ticketId, Comment = comment.Trim(), CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), IsInternal = false }); await context.SaveChangesAsync(); }
        return RedirectToAction(nameof(Details), new { id = ticketId });
    }

    private async Task<TicketFormViewModel> CreateForm(TicketFormViewModel? form = null)
    {
        ViewBag.Customers = new SelectList(await context.Customers.Where(c => c.IsActive).OrderBy(c => c.CompanyName).ToListAsync(), "Id", "CompanyName", form?.CustomerId);
        ViewBag.Categories = new SelectList(await context.TicketCategories.OrderBy(c => c.Name).ToListAsync(), "Id", "Name", form?.CategoryId);
        ViewBag.Priorities = new SelectList(await context.TicketPriorities.OrderBy(p => p.SortOrder).ToListAsync(), "Id", "Name", form?.PriorityId);
        ViewBag.Statuses = new SelectList(await context.TicketStatuses.OrderBy(s => s.Name).ToListAsync(), "Id", "Name", form?.StatusId);
        return form ?? new TicketFormViewModel();
    }
}
