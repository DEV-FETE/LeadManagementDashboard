using LeadManagementDashboard.Entities;

namespace LeadManagementDashboard.Data;

public static class DbInitializer
{
    public static void Seed(ApplicationDbContext context)
    {
        context.Database.EnsureCreated();

        if (context.Statuses.Any()) return; // Database seeded

        // Seed Statuses
        var statuses = new[]
        {
            new Status { Name = "New", DisplayOrder = 1, ColorCode = "#0d6efd", IsActive = true },       // Blue
            new Status { Name = "Contacted", DisplayOrder = 2, ColorCode = "#ffc107", IsActive = true }, // Yellow
            new Status { Name = "Qualified", DisplayOrder = 3, ColorCode = "#198754", IsActive = true }, // Green
            new Status { Name = "Closed", DisplayOrder = 4, ColorCode = "#6c757d", IsActive = true }     // Gray
        };
        context.Statuses.AddRange(statuses);
        context.SaveChanges();

        // Seed Leads
        var leads = new[]
        {
            new Lead { FirstName = "David", LastName = "Miller", Email = "david@digital-post.com", Phone = "+971500000001", Company = "Digital Post Ltd", StatusId = statuses[0].Id, CreatedAt = DateTime.UtcNow.AddDays(-5) },
            new Lead { FirstName = "Adnan", LastName = "Khan", Email = "adnan@techcorp.com", Phone = "+971500000002", Company = "TechCorp Solutions", StatusId = statuses[0].Id, CreatedAt = DateTime.UtcNow.AddDays(-4) },
            new Lead { FirstName = "Sarah", LastName = "Conor", Email = "sarah@cyberdyne.com", Phone = "+971500000003", Company = "Cyberdyne Systems", StatusId = statuses[1].Id, CreatedAt = DateTime.UtcNow.AddDays(-3) },
            new Lead { FirstName = "Michael", LastName = "Scott", Email = "michael@dundermifflin.com", Phone = "+971500000004", Company = "Dunder Mifflin", StatusId = statuses[2].Id, CreatedAt = DateTime.UtcNow.AddDays(-2) },
            new Lead { FirstName = "Elena", LastName = "Rostova", Email = "elena@vanguard.com", Phone = "+971500000005", Company = "Vanguard LLC", StatusId = statuses[3].Id, CreatedAt = DateTime.UtcNow.AddDays(-1) }
        };
        context.Leads.AddRange(leads);
        context.SaveChanges();

        // Seed Activities for at least 2 leads
        var activities = new[]
        {
            new LeadActivity { LeadId = leads[2].Id, FromStatusId = statuses[0].Id, ToStatusId = statuses[1].Id, ChangedAt = DateTime.UtcNow.AddDays(-2), Notes = "Status changed by user" },
            new LeadActivity { LeadId = leads[3].Id, FromStatusId = statuses[0].Id, ToStatusId = statuses[1].Id, ChangedAt = DateTime.UtcNow.AddDays(-2), Notes = "Initial contact established" },
            new LeadActivity { LeadId = leads[3].Id, FromStatusId = statuses[1].Id, ToStatusId = statuses[2].Id, ChangedAt = DateTime.UtcNow.AddDays(-1), Notes = "Status changed by user" }
        };
        context.LeadActivities.AddRange(activities);
        context.SaveChanges();
    }
}