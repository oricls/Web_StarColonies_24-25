namespace StarColonies.Infrastructures.Entities;

public class MissionResource
{
    public int Id { get; set; }
    public int IdMission { get; set; }
    public int IdResource { get; set; }

    // Clés
    public Mission? Mission { get; set; }
    public Resource? Resource { get; set; }
}