namespace LeadManagementDashboard.ViewModels;

public class LeadViewModel
{
    public int Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Company { get; set; } = string.Empty;
    public int StatusId { get; set; }
    public string StatusName { get; set; } = string.Empty;
    public bool CanMoveForward { get; set; }
    public bool CanMoveBackward { get; set; }
    public List<LeadActivityViewModel> Activities { get; set; } = new();
}