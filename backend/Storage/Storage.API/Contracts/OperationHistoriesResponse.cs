namespace Storage.API.Contracts
{
    public record OperationHistoriesResponse(
        Guid Id,
        string operationType,
        string workerName,
        string toolName,
        DateTime Date,
        string Comment);
}
