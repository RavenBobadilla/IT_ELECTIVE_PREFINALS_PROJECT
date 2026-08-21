using System.Diagnostics;
using IT_ELECTIVE_PREFINALS_PROJECT.Models;
using IT_ELECTIVE_PREFINALS_PROJECT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Controllers
{
    public class HomeController(HelpDeskContext context) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var tickets = context.Tickets.Include(t => t.Customer).Include(t => t.Priority).Include(t => t.Status).OrderByDescending(t => t.Id);
            return View(new DashboardViewModel
            {
                TotalTickets = await context.Tickets.CountAsync(),
                OpenTickets = await context.Tickets.CountAsync(t => !t.Status.IsClosed),
                ClosedTickets = await context.Tickets.CountAsync(t => t.Status.IsClosed),
                HighPriorityTickets = await context.Tickets.CountAsync(t => t.Priority.Name == "High" || t.Priority.Name == "Critical"),
                RecentTickets = await tickets.Take(6).ToListAsync()
            });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
