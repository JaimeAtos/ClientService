using Domain.Common;

namespace Domain.Entities
{
    public class LeaveRequest : EntityBaseAuditable<Guid, Guid>
    {
        public Guid PositionId { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ReasonId { get; set; }
        public string? leaveReasonComments { get; set; }
    }
}
