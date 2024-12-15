namespace Storage.API.Contracts
{
    public record OperationHistoriesRequest(
        Guid Id,
        string operationType,
        Guid toolId,
        Guid workerId,
        DateTime Date,
        string Comment);
}
