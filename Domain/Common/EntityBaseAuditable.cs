using Atos.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Common
{
    public class EntityBaseAuditable : IEntityBase<Guid, Guid>, IEntityBaseAuditable<Guid>
    {
        public Guid Id { get; set; }
        public bool State { get; set; }
        public Guid UserCreatorId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Guid ModifiedBy { get; set; }
    }
}
