using Domain.Common;
using System.Data.Common;

namespace Domain.Entities;

public class Client : EntityBaseAuditable
{
    public string Name { get; set; }
    public IEnumerable<Location> Location { get; set; }
    public int CountPositions { get; set; }
}


public class Location
{
    public Guid Id { get; set; }
    public string LocationName { get; set; }
}

