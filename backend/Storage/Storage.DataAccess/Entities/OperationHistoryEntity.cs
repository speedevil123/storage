using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Entities
{
    public class OperationHistoryEntity
    {
        public Guid Id { get; set; }
        public string OperationType { get; set; } = string.Empty;
        public DateTime Date { get; set; } = new DateTime();
        public string Comment { get; set; } = string.Empty;

        //Navigation properties-Foregign Keys
        public Guid ToolId { get; set; }
        public Guid WorkerId { get; set; }
        public virtual Tool Tool { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
