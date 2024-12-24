using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Penalty
    {
        public Penalty(Guid id, double fine, DateTime penaltyDate,bool isPaidOut, Guid toolId, Guid workerId, Rental rental)
        {
            Id = id;
            Fine = fine;
            PenaltyDate = penaltyDate;
            IsPaidOut = isPaidOut;

            ToolId = toolId;
            WorkerId = workerId;
            Rental = rental;
        }

        public Guid Id { get; }
        public double Fine { get; }
        public DateTime PenaltyDate { get; }
        public bool IsPaidOut { get; }

        //Navigation + ForeignKey

        //Составной ключ
        public Guid ToolId { get; }
        public Guid WorkerId { get; }

        public Rental? Rental { get; }
    }
}
