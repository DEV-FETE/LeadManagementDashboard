using LeadManagementDashboard.Data;
using LeadManagementDashboard.Entities;
using LeadManagementDashboard.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace LeadManagementDashboard.Services;

public class LeadService : ILeadService
{
    private readonly ApplicationDbContext _context;

    public LeadService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<StatusViewModel>> GetKanbanBoardAsync()
    {
        var statuses = await _context.Statuses
            .Where(s => s.IsActive)
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync();

        var leads = await _context.Leads
            .Include(l => l.Activities)
            .ToListAsync();

        int minOrder = statuses.Min(s => s.DisplayOrder);
        int maxOrder = statuses.Max(s => s.DisplayOrder);

        return statuses.Select(s => new StatusViewModel
        {
            Id = s.Id,
            Name = s.Name,
            DisplayOrder = s.DisplayOrder,
            ColorCode = s.ColorCode,
            Leads = leads.Where(l => l.StatusId == s.Id).Select(l => new LeadViewModel
            {
                Id = l.Id,
                FullName = $"{l.FirstName} {l.LastName}",
                Email = l.Email,
                Phone = l.Phone,
                Company = l.Company,
                StatusId = l.StatusId,
                StatusName = s.Name,
                CanMoveBackward = s.DisplayOrder > minOrder,
                CanMoveForward = s.DisplayOrder < maxOrder
            }).ToList()
        }).ToList();
    }

    public async Task<(bool Success, string Message)> MoveLeadAsync(int leadId, bool moveForward)
    {
        var lead = await _context.Leads.Include(l => l.Status).FirstOrDefaultAsync(l => l.Id == leadId);
        if (lead == null)
            return (false, "Lead not found.");

        var allStatuses = await _context.Statuses
            .Where(s => s.IsActive)
            .OrderBy(s => s.DisplayOrder)
            .ToListAsync();

        int currentIndex = allStatuses.FindIndex(s => s.Id == lead.StatusId);
        if (currentIndex == -1)
            return (false, "Invalid status state.");

        int targetIndex = moveForward ? currentIndex + 1 : currentIndex - 1;

        // Validation Rules
        if (targetIndex < 0)
            return (false, "Cannot move backward from the first status.");

        if (targetIndex >= allStatuses.Count)
            return (false, "Cannot move forward from the last status.");

        var targetStatus = allStatuses[targetIndex];
        int previousStatusId = lead.StatusId;

        // Update Lead Status
        lead.StatusId = targetStatus.Id;

        // Log Activity
        var activity = new LeadActivity
        {
            LeadId = lead.Id,
            FromStatusId = previousStatusId,
            ToStatusId = targetStatus.Id,
            ChangedAt = DateTime.UtcNow,
            Notes = "Status changed by user"
        };

        _context.LeadActivities.Add(activity);
        await _context.SaveChangesAsync();

        string direction = moveForward ? "forward" : "backward";
        return (true, $"Lead '{lead.FirstName} {lead.LastName}' moved {direction} to '{targetStatus.Name}'.");
    }

    public async Task<LeadViewModel?> GetLeadDetailsAsync(int leadId)
    {
        var lead = await _context.Leads
            .Include(l => l.Status)
            .Include(l => l.Activities)
                .ThenInclude(a => a.FromStatus)
            .Include(l => l.Activities)
                .ThenInclude(a => a.ToStatus)
            .FirstOrDefaultAsync(l => l.Id == leadId);

        if (lead == null) return null;

        return new LeadViewModel
        {
            Id = lead.Id,
            FullName = $"{lead.FirstName} {lead.LastName}",
            Email = lead.Email,
            Phone = lead.Phone,
            Company = lead.Company,
            StatusName = lead.Status.Name,
            Activities = lead.Activities
                .OrderByDescending(a => a.ChangedAt)
                .Select(a => new LeadActivityViewModel
                {
                    Id = a.Id,
                    FromStatusName = a.FromStatus?.Name,
                    ToStatusName = a.ToStatus.Name,
                    ChangedAt = a.ChangedAt,
                    Notes = a.Notes
                }).ToList()
        };
    }
}