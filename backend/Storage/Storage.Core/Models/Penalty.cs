using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Penalty
    {
        public Penalty(Guid id, double fine, DateTime penaltyDate, Guid rentalId, Rental rental)
        {
            Id = id;
            Fine = fine;
            PenaltyDate = penaltyDate;
            RentalId = rentalId;
            Rental = rental;
        }

        public Guid Id { get; }
        public double Fine { get; }
        public DateTime PenaltyDate { get; }

        //Navigation + ForeignKey
        public Guid RentalId { get; }
        public Rental? Rental { get; }
    }
}
