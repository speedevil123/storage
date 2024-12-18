using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Rental
    {
        public Rental(Guid workerId, Guid toolId, 
            DateTime startDate, DateTime returnDate, DateTime endDate, string status, Worker worker, Tool tool, int toolQuantity)
        {
            WorkerId = workerId;
            ToolId = toolId;
            StartDate = startDate;
            ReturnDate = returnDate;
            EndDate = endDate;
            Status = status;
            Worker = worker;
            Tool = tool;
            ToolQuantity = toolQuantity;
        }
        public DateTime StartDate { get; } = new DateTime();
        public DateTime ReturnDate { get; } = new DateTime();
        public DateTime EndDate { get; } = new DateTime();

        //Если просрочен то триггером поставить статус просрочен и сгенерировать строку с Penalty
        public string Status { get; } = string.Empty; // Статус (активен, завершен, просрочен)
        public int ToolQuantity { get; set; }

        //Navigation + ForeignKey
        public Guid WorkerId { get; }
        public Guid ToolId { get; }

        public Worker? Worker { get; } 
        public Tool? Tool { get; } 

        public List<Penalty> Penalties { get; } = new List<Penalty>();
    }
}
