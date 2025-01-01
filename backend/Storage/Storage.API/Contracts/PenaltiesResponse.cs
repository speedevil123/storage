namespace Storage.API.Contracts
{
    public record PenaltiesResponse(
        Guid Id,
        double Fine,
        DateTime PenaltyDate,
        bool IsPaidOut,
        Guid WorkerId,
        Guid ToolId,
        string WorkerName,
        string ToolName);
}
