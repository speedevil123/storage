using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Entities
{
    public class PenaltyEntity
    {
        public Guid Id { get; set; }
        public double Fine { get; set; }
        public DateTime PenaltyDate { get; set; }
        public bool IsPaidOut {  get; set; }

        public Guid ToolId { get; set; }
        public Guid WorkerId { get; set; }

        //Navigation 
        public RentalEntity? Rental { get; set; }
    }
}
