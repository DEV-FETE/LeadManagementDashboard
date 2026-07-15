namespace LeadManagementDashboard.ViewModels;

public class StatusViewModel
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int DisplayOrder { get; set; }
    public string ColorCode { get; set; } = string.Empty;
    public List<LeadViewModel> Leads { get; set; } = new();
}