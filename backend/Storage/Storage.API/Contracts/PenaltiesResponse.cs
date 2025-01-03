namespace Storage.API.Contracts
{
    public record PenaltiesResponse(
        Guid Id,
        double Fine,
        DateTime PenaltyDate,
        bool IsPaidOut,
        Guid RentalId,
        string WorkerName,
        string ToolName);
}
