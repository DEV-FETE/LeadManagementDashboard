using LeadManagementDashboard.ViewModels;

namespace LeadManagementDashboard.Services;

public interface ILeadService
{
    Task<List<StatusViewModel>> GetKanbanBoardAsync();
    Task<(bool Success, string Message)> MoveLeadAsync(int leadId, bool moveForward);
    Task<LeadViewModel?> GetLeadDetailsAsync(int leadId);
}