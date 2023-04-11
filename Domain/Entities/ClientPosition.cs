using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ClientPosition : EntityBaseAuditable<Guid, Guid>
    {
        public Guid ClientId { get; set; }
        public Guid PositionId { get; set; }
        public string PositionName { get; set; }
        public string RomaId { get; set; }
    }
}
