using System.ComponentModel.DataAnnotations;
using StarColonies.Domains;

namespace StarColonies.Web.Validators
{
    /// <summary>
    /// Validateur optimisé qui utilise directement une liste de professions au format JSON
    /// </summary>
    public class ProfessionCompositionValidator : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrEmpty(value.ToString()))
            {
                return new ValidationResult("Aucune profession sélectionnée.");
            }

            var professionsJson = value.ToString();
            var (isValid, errorMessage) = Team.ValidateTeamProfessionsJson(professionsJson!);
            
            return !isValid ? new ValidationResult(errorMessage) : ValidationResult.Success;
        }
    }
}