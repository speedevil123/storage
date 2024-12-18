using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Tool
    {
        public Tool(Guid id, Guid modelId, Guid manufacturerId, int quantity)
        {
            Id = id;
            ModelId = modelId;
            ManufacturerId = manufacturerId;
            Quantity = quantity;
        }
        public Guid Id { get; }
        public Guid ModelId { get; }
        public Guid ManufacturerId { get; }
        public int Quantity { get; } = 0;
        public List<Rental> Rentals { get; } = new List<Rental>();

    }
}
