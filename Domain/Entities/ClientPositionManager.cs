using Domain.Common;

namespace Domain.Entities;

public class ClientPositionManager : EntityBaseAuditable<Guid, Guid>
{
    public Guid ClientPositionId { get; set; }
    public Guid ResourceId { get; set; }
    public string Resource { get; set; }
}
