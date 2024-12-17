using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Rental
    {
        public Rental() { }
        public Rental(Guid workerId, Guid toolId, 
            DateTime startDate, DateTime returnDate, string status)
        {
            WorkerId = workerId;
            ToolId = toolId;
            StartDate = startDate;
            ReturnDate = returnDate;
            Status = status;
        }
        public DateTime StartDate { get; } = new DateTime();
        public DateTime ReturnDate { get; } = new DateTime();
        public string Status { get; } = string.Empty; // Статус (активен, завершен, просрочен)
        
        //Navigation Properties-ForeignKeys
        public Guid WorkerId { get; }
        public Guid ToolId { get; }

        public Worker? Worker { get; } 
        public Tool? Tool { get; } 
    }
}
