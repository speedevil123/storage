using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Penalty
    {
        public Penalty(Guid id, double fine, DateTime penaltyDate,bool isPaidOut, Guid rentalId, Rental rental)
        {
            Id = id;
            Fine = fine;
            PenaltyDate = penaltyDate;
            IsPaidOut = isPaidOut;

            RentalId = rentalId;
            Rental = rental;
        }

        public Guid Id { get; }
        public double Fine { get; }
        public DateTime PenaltyDate { get; }
        public bool IsPaidOut { get; }

        //Navigation + ForeignKey

        //Составной ключ
        public Guid RentalId { get; }

        public Rental? Rental { get; }
    }
}
