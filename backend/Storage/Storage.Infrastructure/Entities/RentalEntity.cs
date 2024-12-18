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
        public DateTime StartDate { get; set; } = new DateTime();
        public DateTime ReturnDate { get; set; } = new DateTime();
        public DateTime EndDate { get; set; } = new DateTime();

        //Если просрочен то триггером поставить статус просрочен и сгенерировать строку с Penalty
        public string Status { get; set; } = string.Empty; // Статус (активен, завершен, просрочен)

        //Navigation + ForeignKey
        public Guid WorkerId { get; set; }
        public Guid ToolId { get; set; }

        public WorkerEntity? Worker { get; set; }
        public ToolEntity? Tool { get; set; }

        public List<PenaltyEntity> Penalties { get; } = new List<PenaltyEntity>();

    }
}
