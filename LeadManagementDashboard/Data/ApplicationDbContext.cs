using LeadManagementDashboard.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace LeadManagementDashboard.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Status> Statuses => Set<Status>();
    public DbSet<Lead> Leads => Set<Lead>();
    public DbSet<LeadActivity> LeadActivities => Set<LeadActivity>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Status configuration
        modelBuilder.Entity<Status>(b =>
        {
            b.HasKey(s => s.Id);
            b.Property(s => s.Name).IsRequired().HasMaxLength(100);
            b.Property(s => s.ColorCode).HasMaxLength(20);
        });

        // Lead configuration
        modelBuilder.Entity<Lead>(b =>
        {
            b.HasKey(l => l.Id);
            b.Property(l => l.FirstName).IsRequired().HasMaxLength(100);
            b.Property(l => l.LastName).IsRequired().HasMaxLength(100);
            b.Property(l => l.Email).IsRequired().HasMaxLength(255);
            b.Property(l => l.Phone).HasMaxLength(50);
            b.Property(l => l.Company).HasMaxLength(150);

            b.HasOne(l => l.Status)
             .WithMany(s => s.Leads)
             .HasForeignKey(l => l.StatusId)
             .OnDelete(DeleteBehavior.Restrict);
        });

        // Activity configuration
        modelBuilder.Entity<LeadActivity>(b =>
        {
            b.HasKey(a => a.Id);
            b.Property(a => a.Notes).HasMaxLength(500);

            b.HasOne(a => a.Lead)
             .WithMany(l => l.Activities)
             .HasForeignKey(a => a.LeadId)
             .OnDelete(DeleteBehavior.Cascade);

            b.HasOne(a => a.FromStatus)
             .WithMany()
             .HasForeignKey(a => a.FromStatusId)
             .OnDelete(DeleteBehavior.Restrict);

            b.HasOne(a => a.ToStatus)
             .WithMany()
             .HasForeignKey(a => a.ToStatusId)
             .OnDelete(DeleteBehavior.Restrict);
        });
    }
}