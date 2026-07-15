namespace LeadManagementDashboard.Entities
{
    public class Status
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int DisplayOrder { get; set; }
        public string ColorCode { get; set; } = "#cccccc";
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public ICollection<Lead> Leads { get; set; } = new List<Lead>();
    }
}
