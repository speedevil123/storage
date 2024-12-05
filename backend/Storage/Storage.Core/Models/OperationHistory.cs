using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class OperationHistory
    {
        public OperationHistory(Guid id, string operationType, Guid toolId, Guid workerId,
            DateTime date, string comment, Tool tool, Worker worker) 
        {
            Id = id;
            OperationType = operationType;
            ToolId = toolId;
            WorkerId = workerId;
            Date = date;
            Comment = comment;

            Tool = tool;
            Worker = worker;
        }
        public Guid Id { get;}
        public string OperationType { get; } = string.Empty;
        public DateTime Date { get; } = new DateTime();
        public string Comment { get;} = string.Empty;
        
        //Navigation properties-Foregign Keys
        public Guid ToolId { get; }
        public Guid WorkerId { get; }

        public virtual Tool Tool { get;}
        public virtual Worker Worker { get;} 
    }
}
