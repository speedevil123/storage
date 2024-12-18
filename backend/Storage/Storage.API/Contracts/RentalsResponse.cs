namespace Storage.API.Contracts
{
    public record RentalsResponse(
        Guid WorkerId,
        Guid ToolId,
        string WorkerName,
        string ToolName,
        DateTime StartDate,
        DateTime ReturnDate,
        DateTime EndDate,
        string Status,
        int ToolQuantity);
}
