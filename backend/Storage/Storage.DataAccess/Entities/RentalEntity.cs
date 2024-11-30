using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Entities
{
    public class RentalEntity
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; } = new DateTime();
        public DateTime? ReturnDate { get; set; } = new DateTime();
        public string Status { get; set; } = string.Empty; // Статус (активен, завершен, просрочен)

        //Navigation Properties-ForeignKeys
        public Guid WorkerId { get; set; }
        public Guid ToolId { get; set; }

        public Worker Worker { get; set; }
        public Tool Tool { get; set; }
    }
}
