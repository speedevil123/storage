namespace Storage.API.Contracts
{
    public record RentalsResponse(
        Guid WorkerId,
        Guid ToolId,
        string WorkerName,
        string ToolName,
        string StartDate,
        string ReturnDate,
        string EndDate,
        string Status,
        int ToolQuantity);
}
