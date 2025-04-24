using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages;

[Authorize(Roles = "Admin")]
public class AdminModel(IAdminRepository adminRepository) : PageModel
{
    public List<Log> Logs { get; set; } = [];

    public async Task OnGetAsync()
    {
        Logs = (await adminRepository.GetLogs()).ToList();
    }
}