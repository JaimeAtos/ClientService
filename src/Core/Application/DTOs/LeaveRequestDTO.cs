namespace Application.DTOs
{
    public class LeaveRequestDTO
    {
        public Guid ClientPositionId { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ReasonId { get; set; }
        public string LeaveReasonComments { get; set; } = null!;
        public bool State { get; set; }
    }
}
