using Atos.Core.Commons;

namespace Domain.Entities;

public class Client : EntityBaseAuditable<Guid, Guid>
{
    public string Name { get; set; } = null!;
    public Guid LocationId { get; set; }
    public string LocationName { get; set; } = null!;
    public int CountPositions { get; set; }
    public ICollection<ClientPosition> ClientPositions { get; set; } = new List<ClientPosition>();
}



