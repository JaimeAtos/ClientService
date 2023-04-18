namespace Application.DTOs
{
    public class ClientPositionDTO
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid PositionId { get; set; }
        public bool State { get; set; }
        public string PositionName { get; set; }
        public string RomaId { get; set; }
    }
}
