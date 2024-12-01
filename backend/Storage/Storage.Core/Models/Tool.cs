using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Tool
    {
        public const int MAX_STR_LENGTH = 50;
        public Tool(Guid id, string type, string model, string manufacturer, int quantity, bool isTaken)
        {
            Id = id;
            Type = type;
            Model = model;
            Manufacturer = manufacturer;
            Quantity = quantity;
            IsTaken = isTaken;
        }

        public Guid Id { get; }
        public string Type { get; } = string.Empty;
        public string Model { get; } = string.Empty;
        public string Manufacturer { get; } = string.Empty;
        public int Quantity { get; } = 0;
        public bool IsTaken { get; } = false;
        public List<Worker> Workers { get; } = new List<Worker>();
        public List<Rental> Rentals { get; } = new List<Rental>();
    }
}
