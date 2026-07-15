namespace LeadManagementDashboard.ViewModels;

public class LeadActivityViewModel
{
    public int Id { get; set; }
    public string? FromStatusName { get; set; }
    public string ToStatusName { get; set; } = string.Empty;
    public DateTime ChangedAt { get; set; }
    public string Notes { get; set; } = string.Empty;
}