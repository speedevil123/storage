using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Entities
{
    public class ToolEntity
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; } = 0;

        //Navigation + ForeignKey
        public Guid ModelId { get; set; }
        public Guid ManufacturerId { get; set; }
        public Model? Model { get; set; }
        public Manufacturer? Manufacturer { get; set;}

        public List<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
