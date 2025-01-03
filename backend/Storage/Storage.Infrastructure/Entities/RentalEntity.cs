// Infrastructure Entities
namespace Storage.Infrastructure.Entities
{
    public class RentalEntity
    {
        public Guid Id { get; set; } // Новый первичный ключ
        public DateTime StartDate { get; set; } = new DateTime();
        public DateTime ReturnDate { get; set; } = new DateTime();
        public DateTime EndDate { get; set; } = new DateTime();

        public string Status { get; set; } = string.Empty; // Статус (активен, завершен, просрочен)
        public int ToolQuantity { get; set; }

        // Navigation + ForeignKey
        public Guid WorkerId { get; set; }
        public Guid ToolId { get; set; }

        public WorkerEntity? Worker { get; set; }
        public ToolEntity? Tool { get; set; }

        public List<PenaltyEntity> Penalties { get; } = new List<PenaltyEntity>();
    }
}