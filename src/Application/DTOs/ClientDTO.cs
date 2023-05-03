namespace Application.DTOs
{
    public class ClientDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid LocationId { get; set; }
        public string LocationName { get; set; }
        public int CountPositions { get; set; }
        public bool State { get; set; }
    }
}
