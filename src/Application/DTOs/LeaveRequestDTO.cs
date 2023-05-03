namespace Application.DTOs
{
    public class LeaveRequestDTO
    {
        public Guid PositionId { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ReasonId { get; set; }
        public string? LeaveReasonComments { get; set; }
        public bool State { get; set; }
    }
}
