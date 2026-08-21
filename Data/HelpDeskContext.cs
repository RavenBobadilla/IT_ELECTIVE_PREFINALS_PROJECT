using IT_ELECTIVE_PREFINALS_PROJECT.Models;
using Microsoft.EntityFrameworkCore;

namespace IT_ELECTIVE_PREFINALS_PROJECT.Data;

public class HelpDeskContext(DbContextOptions<HelpDeskContext> options) : DbContext(options)
{
    public DbSet<Department> Departments => Set<Department>(); public DbSet<Employee> Employees => Set<Employee>(); public DbSet<Team> Teams => Set<Team>(); public DbSet<TeamMember> TeamMembers => Set<TeamMember>(); public DbSet<Customer> Customers => Set<Customer>(); public DbSet<Ticket> Tickets => Set<Ticket>(); public DbSet<TicketStatus> TicketStatuses => Set<TicketStatus>(); public DbSet<TicketPriority> TicketPriorities => Set<TicketPriority>(); public DbSet<TicketCategory> TicketCategories => Set<TicketCategory>(); public DbSet<TicketAssignment> TicketAssignments => Set<TicketAssignment>(); public DbSet<TicketComment> TicketComments => Set<TicketComment>(); public DbSet<Tag> Tags => Set<Tag>(); public DbSet<TicketTag> TicketTags => Set<TicketTag>(); public DbSet<TicketAttachment> TicketAttachments => Set<TicketAttachment>();
    protected override void OnModelCreating(ModelBuilder b)
    {
        b.Entity<TeamMember>().HasKey(x => new { x.TeamId, x.EmployeeId }); b.Entity<TicketAssignment>().HasKey(x => new { x.TicketId, x.EmployeeId }); b.Entity<TicketTag>().HasKey(x => new { x.TicketId, x.TagId });
        b.Entity<Employee>().HasOne(x => x.Department).WithMany(x => x.Employees).HasForeignKey(x => x.DepartmentId);
        b.Entity<Team>().HasOne(x => x.Department).WithMany(x => x.Teams).HasForeignKey(x => x.DepartmentId);
        b.Entity<TeamMember>().HasOne(x => x.Team).WithMany(x => x.TeamMembers).HasForeignKey(x => x.TeamId); b.Entity<TeamMember>().HasOne(x => x.Employee).WithMany(x => x.TeamMemberships).HasForeignKey(x => x.EmployeeId);
        b.Entity<TicketCategory>().HasOne(x => x.ParentCategory).WithMany(x => x.Subcategories).HasForeignKey(x => x.ParentCategoryId);
        b.Entity<Ticket>().HasOne(x => x.Customer).WithMany(x => x.Tickets).HasForeignKey(x => x.CustomerId); b.Entity<Ticket>().HasOne(x => x.Category).WithMany(x => x.Tickets).HasForeignKey(x => x.CategoryId); b.Entity<Ticket>().HasOne(x => x.Priority).WithMany(x => x.Tickets).HasForeignKey(x => x.PriorityId); b.Entity<Ticket>().HasOne(x => x.Status).WithMany(x => x.Tickets).HasForeignKey(x => x.StatusId);
        b.Entity<TicketAssignment>().HasOne(x => x.Ticket).WithMany(x => x.Assignments).HasForeignKey(x => x.TicketId); b.Entity<TicketAssignment>().HasOne(x => x.Employee).WithMany(x => x.TicketAssignments).HasForeignKey(x => x.EmployeeId);
        b.Entity<TicketComment>().HasOne(x => x.Ticket).WithMany(x => x.Comments).HasForeignKey(x => x.TicketId); b.Entity<TicketComment>().HasOne(x => x.Employee).WithMany(x => x.TicketComments).HasForeignKey(x => x.EmployeeId);
        b.Entity<TicketTag>().HasOne(x => x.Ticket).WithMany(x => x.TicketTags).HasForeignKey(x => x.TicketId); b.Entity<TicketTag>().HasOne(x => x.Tag).WithMany(x => x.TicketTags).HasForeignKey(x => x.TagId); b.Entity<TicketAttachment>().HasOne(x => x.Ticket).WithMany(x => x.Attachments).HasForeignKey(x => x.TicketId);
    }
}
