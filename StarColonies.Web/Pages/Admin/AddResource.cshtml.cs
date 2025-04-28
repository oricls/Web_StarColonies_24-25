using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using StarColonies.Domains;
using StarColonies.Domains.Repositories;

namespace StarColonies.Web.Pages.Admin;

public class AddResourceInputModel
{
    [Required(ErrorMessage = "Le nom est requis")]
    [StringLength(50, ErrorMessage = "Le nom ne peut pas dépasser 50 caractères")]
    public string Name { get; set; }

    [StringLength(500, ErrorMessage = "La description ne peut pas dépasser 500 caractères")]
    public string Description { get; set; }

    [Required(ErrorMessage = "Le type de ressource est requis")]
    public int ResourceTypeId { get; set; }
}

[Authorize (Roles = "Admin")]
public class AddResource(IColonRepository colonRepository) : PageModel
{
    public async Task OnGetAsync()
    {
        TypeResourcesAvailable = await colonRepository.GetAllTypeResourcesAsync();
    }
    
    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            // Recharge les types de ressources si la validation échoue
            TypeResourcesAvailable = await colonRepository.GetAllTypeResourcesAsync();
            return Page();
        }

        try
        {
            // Création de la ressource
            var resource = new Resource()
            {
                Name = AddResourceInputModel.Name,
                Description = AddResourceInputModel.Description,
                Type = new TypeResource()
                {
                    Id = AddResourceInputModel.ResourceTypeId
                }
            };

            // Enregistrement de la ressource
            await colonRepository.AddResourceAsync(resource);

            // Redirection vers une page de succès ou liste des ressources
            return RedirectToPage("/Admin/Resources");
        }
        catch (Exception ex)
        {
            ModelState.AddModelError(string.Empty, "Une erreur est survenue lors de la création de la ressource.");
            // Recharge les types de ressources en cas d'erreur
            TypeResourcesAvailable = await colonRepository.GetAllTypeResourcesAsync();
            return Page();
        }
    }

    [BindProperty]
    public AddResourceInputModel AddResourceInputModel { get; set; } = new AddResourceInputModel();

    public IReadOnlyCollection<TypeResource> TypeResourcesAvailable { get; set; } = new List<TypeResource>();
}