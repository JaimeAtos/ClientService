using Atos.Core.Commons;

namespace Domain.Entities
{
    public class ClientPosition : EntityBaseAuditable<Guid, Guid>
    {
        public Guid ClientId { get; set; }
        public Guid PositionId { get; set; }
        public string PositionDescription { get; set; } = null!;
        public Guid CurrentStateId { get; set; }
        public string CurrentStateName { get; set; } = null!;
        public Client Client { get; set; } = null!;
    }
}
