using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;
using static System.String;

namespace StarColonies.Web.Pages.Admin;

[Authorize (Roles = "Admin")]
public class AddMission(IMissionRepository missionRepository) : PageModel
{
    [BindProperty]
    public AddMissionInputModel AddMissionInputModel { get; set; } = new();
    
    public async Task OnGetAsync()
    {
        AvailableBestiaires = await missionRepository.GetAllBestiaires();
        AvailableResources = await missionRepository.GetAllResourcesAsync();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            AvailableBestiaires = await missionRepository.GetAllBestiaires();
            return Page();
        }

        // Gérer l'enregistrement de l'image
        var uploadsFolder = Path.Combine("wwwroot", "uploads", "missions");
        Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid() + Path.GetExtension(AddMissionInputModel.MissionImage.FileName);
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        await using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await AddMissionInputModel.MissionImage.CopyToAsync(stream);
        }

        var selectedBestiaires = AddMissionInputModel.SelectedMonsters
            .Select(id => new Bestiaire { Id = id })
            .ToList();
        
        var selectedResources = AddMissionInputModel.SelectedResources
            .Select(id => new Resource { Id = id })
            .ToList();

        var mission = new Mission
        {
            Name = AddMissionInputModel.MissionName,
            Description = AddMissionInputModel.MissionDescription,
            Image = uniqueFileName,
            Bestiaires = selectedBestiaires,
            Resources = selectedResources
        };

        await missionRepository.AddMission(mission);
        return RedirectToPage("/Admin/Missions");
    }


    public IReadOnlyList<Bestiaire> AvailableBestiaires { get; set; } = new List<Bestiaire>();
    
    public IReadOnlyList<Resource> AvailableResources { get; set; } = new List<Resource>();
}

public class AddMissionInputModel
{
    [Required]
    [StringLength(100, MinimumLength = 3)]
    public string MissionName { get; set; } = Empty;
    
    [Required]
    [StringLength(500, MinimumLength = 10)]
    public string MissionDescription { get; set; } = Empty;
    
    [Required]
    public IFormFile MissionImage { get; set; }
    
    [Required(ErrorMessage = "Sélectionnez au moins un monstre")]
    [MinLength(1, ErrorMessage = "Sélectionnez au moins un monstre")]
    public List<int> SelectedMonsters { get; set; } = new();
    
    [Required(ErrorMessage = "Sélectionnez au moins une ressource")]
    [MinLength(1, ErrorMessage = "Sélectionnez au moins une ressource")]
    public List<int> SelectedResources { get; set; } = new();
}