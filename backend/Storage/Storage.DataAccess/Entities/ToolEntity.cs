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
        public string Type { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public int Quantity { get; set; } = 0;
        public bool IsTaken { get; set; } = false;
        public List<WorkerEntity> Workers { get; } = new List<WorkerEntity>();
        public List<RentalEntity> Rentals { get; } = new List<RentalEntity>();
        public List<OperationHistoryEntity> OperationHistories { get; } = new List<OperationHistoryEntity>();
    }
}
