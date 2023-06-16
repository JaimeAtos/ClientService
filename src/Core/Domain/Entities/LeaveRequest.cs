using Atos.Core.Commons;

namespace Domain.Entities
{
    public class LeaveRequest : EntityBaseAuditable<Guid, Guid>
    {
        public Guid ClientPositionId { get; set; }
        public Guid ResourceId { get; set; }
        public Guid ReasonId { get; set; }
        public string LeaveReasonComments { get; set; } = null!;
        public ClientPosition ClientPosition { get; set; } = null!;
    }
}
