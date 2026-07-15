using LeadManagementDashboard.Services;
using LeadManagementDashboard.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LeadManagementDashboard.Pages;

public class IndexModel : PageModel
{
    private readonly ILeadService _leadService;

    public IndexModel(ILeadService leadService)
    {
        _leadService = leadService;
    }

    public List<StatusViewModel> Columns { get; set; } = new();

    [TempData]
    public string? ToastMessage { get; set; }

    [TempData]
    public string? ToastType { get; set; }

    public async Task OnGetAsync()
    {
        Columns = await _leadService.GetKanbanBoardAsync();
    }

    public async Task<IActionResult> OnPostMoveAsync(int leadId, bool moveForward)
    {
        var result = await _leadService.MoveLeadAsync(leadId, moveForward);

        ToastMessage = result.Message;
        ToastType = result.Success ? "success" : "danger";

        return RedirectToPage();
    }

    public async Task<IActionResult> OnGetLeadDetailsAsync(int id)
    {
        var leadDetails = await _leadService.GetLeadDetailsAsync(id);
        if (leadDetails == null) return NotFound();

        return new JsonResult(leadDetails);
    }
}