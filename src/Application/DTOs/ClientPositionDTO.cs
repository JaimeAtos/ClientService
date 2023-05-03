namespace Application.DTOs
{
    public class ClientPositionDTO
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; }
        public Guid PositionId { get; set; }
        public bool State { get; set; }
        public string PositionDescription { get; set; }
        public Guid CurrentStateID { get; set; }
        public string CurrentStateName { get; set; }
    }
}
