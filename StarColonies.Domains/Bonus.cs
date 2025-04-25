namespace StarColonies.Domains
{
    public class Bonus
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DateAchat { get; set; }
        public DateTime DateExpiration { get; set; }
        public string IconUrl { get; set; } = string.Empty; 
        public BonusEffectType EffectType { get; set; } = BonusEffectType.None;
        public IList<BonusResource> Resources { get; set; } = new List<BonusResource>();
        
        public bool IsActive()
        {
            var now = DateTime.Now;
            return DateAchat <= now && DateExpiration >= now;
        }
        
        // Méthode pour appliquer le bonus à une mission
        public void ApplyToMission(Mission mission, Team team)
        {
            if (!IsActive())
            {
                return;
            }

            // Utiliser le type d'effet pour déterminer quelle action prendre
            switch (EffectType)
            {
                    case BonusEffectType.DoubleStrength:
                    ApplyStrengthBonus(team);
                    break;
            
                case BonusEffectType.DoubleEndurance:
                    ApplyEnduranceBonus(team);
                    break;
            
                case BonusEffectType.IncreaseLevel:
                    ApplyLevelBonus(team);
                    break;
            
                case BonusEffectType.DoubleResources:
                    // Rien à faire ici, car ce bonus est appliqué après la mission 
                    break;
                case BonusEffectType.ExperienceBoost:
                    // Rien à faire ici, car ce bonus est appliqué après la mission
                    break;
                case BonusEffectType.None:
                    break;
                
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public bool ApplyExperienceBonus()
        {
            return IsActive() && EffectType == BonusEffectType.ExperienceBoost;
        }
        
        // Méthode pour appliquer ce bonus aux ressources obtenues
        public void ApplyToResources(IList<Resource> resources)
        {
            if (!IsActive())
                return;
                
            if (EffectType == BonusEffectType.DoubleResources)
            {
                // Doubler la quantité de chaque ressource
                foreach (var resource in resources)
                {
                    resource.Quantity *= 2;
                }
            }
        }
        
        // Méthodes spécifiques pour chaque type de bonus
        private static void ApplyStrengthBonus(Team team)
        {
            // Doubler la force de l'équipe
            team.TotalStrength *= 2;
        }
        
        private static void ApplyEnduranceBonus(Team team)
        {
            // Doubler l'endurance de l'équipe
            team.TotalEndurance *= 2;
        }
        
        private static void ApplyLevelBonus(Team team)
        {
            // Augmenter virtuellement le niveau de chaque membre
            // Ce qui augmente force et endurance hehe
            team.TotalStrength += team.MemberCount;
            team.TotalEndurance += team.MemberCount;
        }
    }
}