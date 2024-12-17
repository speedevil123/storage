namespace Storage.API.Contracts
{
    public record RentalsRequest(
        Guid workerId,
        Guid toolId,
        string workerName,
        string toolName,
        DateTime startDate,
        DateTime returnDate,
        string status);
}
