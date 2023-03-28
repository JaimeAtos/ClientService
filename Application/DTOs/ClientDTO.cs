using Domain.Entities;

namespace Application.DTOs
{
    public class ClientDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Location> Location { get; set; }
        public int CountPositions { get; set; }
    }
}
