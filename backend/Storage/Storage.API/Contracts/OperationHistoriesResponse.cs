namespace Storage.API.Contracts
{
    public record OperationHistoriesResponse(
        Guid Id,
        string operationType,
        string toolName,
        string workerName,
        DateTime Date,
        string Comment);
}
