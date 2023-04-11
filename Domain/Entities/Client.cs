using Domain.Common;
using System.Data.Common;

namespace Domain.Entities;

public class Client : EntityBaseAuditable<Guid, Guid>
{
    public string Name { get; set; }
    public Guid LocationId { get; set; }
    public string LocationName { get; set; }
    public int CountPositions { get; set; }
}



