using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

[Authorize(Roles = "Admin")]
public class AdminModel(ILogRepository logRepository) : PageModel
{
    public IList<Log> Logs { get; set; } = [];
    
    [BindProperty(SupportsGet = true)]
    public DateTime SelectedDate { get; set; } = DateTime.Today;
    
    public async Task OnGetAsync()
    {
        Logs = await logRepository.GetLogsByDate(SelectedDate);
    }
}