using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class ClientPositionManagerDTO
    {
        public Guid Id { get; set; }
        public Guid ClientPositionId { get; set; }
        public Guid ResourceId { get; set; }
        public string Resource { get; set; }
        public bool State { get; set; }
    }
}
