namespace Application.DTOs
{
    public class ClientPositionManagerDTO
    {
        public Guid Id { get; set; }
        public Guid ClientPositionId { get; set; }
        public Guid ResourceId { get; set; }
        public string Resource { get; set; } = null!;
        public bool State { get; set; }
    }
}
