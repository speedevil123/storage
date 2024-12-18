using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Penalty
    {
        public Guid Id { get; set; }
        public double Fine { get; set; }
        public DateTime PenaltyDate { get; set; }
        //Navigation + ForeignKey
        public Guid RentalId { get; set; }
        public Rental Rental { get; set; }
    }
}
