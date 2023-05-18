namespace Application.DTOs
{
    public class ClientDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = null!;
        public Guid LocationId { get; set; }
        public string LocationName { get; set; } = null!;
        public int CountPositions { get; set; }
        public bool State { get; set; }
        public ICollection<ClientPositionDTO> ClientPositions { get; set; } = new List<ClientPositionDTO>();
    }
}
