using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace StarColonies.Domains;

public class Team
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Logo { get; set; }
    
    public string Baniere { get; set; }
    public int MemberCount { get; set; }
    public int AverageLevel { get; set; }
    public string CreatorId { get; set; }

    public bool IsSelectedForMissions { get; set; }
    
    /// <summary>
    /// Vérifie si la composition d'une équipe est valide en utilisant directement les professions
    /// </summary>
    /// <param name="professions">Liste des professions des membres de l'équipe</param>
    /// <returns>Tuple contenant un booléen indiquant si la composition est valide et un message d'erreur le cas échéant</returns>
    public static (bool IsValid, string ErrorMessage) ValidateTeamProfessions(IEnumerable<string> professions)
    {
        var professionList = professions.ToList();
        
        // 1. Vérifier le nombre de membres (entre 4 et 5)
        if (professionList.Count < 4 || professionList.Count > 5)
        {
            return (false, "L'équipe doit comporter entre 4 et 5 membres.");
        }
        
        // 2. Compter les professions
        var professionCounts = professionList.GroupBy(p => p)
                                       .ToDictionary(g => g.Key, g => g.Count());
        
        // 3. Vérifier qu'il n'y a pas plus de deux membres de la même profession
        foreach (var profession in professionCounts)
        {
            if (profession.Value > 2)
            {
                return (false, $"L'équipe ne peut pas avoir plus de deux membres de la profession {profession.Key}.");
            }
        }
        
        // 4. Vérifier la présence d'un médecin, d'un soldat et un scientifique
        if (!professionCounts.ContainsKey("Médecin") || professionCounts["Médecin"] < 1)
        {
            return (false, "L'équipe doit comporter au moins un médecin.");
        }
        
        if (!professionCounts.ContainsKey("Soldat") || professionCounts["Soldat"] < 1)
        {
            return (false, "L'équipe doit comporter au moins un soldat.");
        }
        
        if (!professionCounts.ContainsKey("Scientifique") || professionCounts["Scientifique"] < 1)
        {
            return (false, "L'équipe doit comporter au moins un scientifique.");
        }
        
        // Si toutes les validations passent
        return (true, string.Empty);
    }
    
    /// <summary>
    /// Vérifie si la composition d'une équipe est valide à partir d'une chaîne JSON de professions
    /// </summary>
    /// <param name="professionsJson">Chaîne JSON contenant les professions des membres de l'équipe</param>
    /// <returns>Tuple contenant un booléen indiquant si la composition est valide et un message d'erreur le cas échéant</returns>
    public static (bool IsValid, string ErrorMessage) ValidateTeamProfessionsJson(string professionsJson)
    {
        try
        {
            var professions = JsonSerializer.Deserialize<List<string>>(professionsJson);
            return ValidateTeamProfessions(professions!);
        }
        catch (Exception ex)
        {
            return (false, $"Erreur lors de la validation des professions: {ex.Message}");
        }
    }
}