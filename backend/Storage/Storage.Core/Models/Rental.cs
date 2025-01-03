namespace Storage.Core.Models
{
    public class Rental
    {
        public Rental(Guid id, Guid workerId, Guid toolId,
            DateTime startDate, DateTime returnDate, DateTime endDate, string status, Worker worker, Tool tool, int toolQuantity)
        {
            Id = id;
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

        public Guid Id { get; } // Новый первичный ключ
        public DateTime StartDate { get; } = new DateTime();
        public DateTime ReturnDate { get; } = new DateTime();
        public DateTime EndDate { get; } = new DateTime();

        public string Status { get; } = string.Empty; // Статус (активен, завершен, просрочен)
        public int ToolQuantity { get; set; }

        // Navigation + ForeignKey
        public Guid WorkerId { get; }
        public Guid ToolId { get; }

        public Worker? Worker { get; }
        public Tool? Tool { get; }

        public List<Penalty> Penalties { get; } = new List<Penalty>();
    }
}