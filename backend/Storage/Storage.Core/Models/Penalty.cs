using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Penalty
    {
        public Penalty(Guid id, double fine, DateTime penaltyDate, Rental rental)
        {
            Id = id;
            Fine = fine;
            PenaltyDate = penaltyDate;
            Rental = rental;
        }

        public Guid Id { get; }
        public double Fine { get; }
        public DateTime PenaltyDate { get; }

        public Guid ToolId { get; }
        public Guid WorkerId { get; }

        //Navigation
        public Rental? Rental { get; }
    }
}
