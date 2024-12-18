﻿namespace Storage.API.Contracts
{
    public record PenaltiesResponse(
        Guid Id,
        double Fine,
        DateTime PenaltyDate,
        Guid WorkerId,
        Guid ToolId,
        string WorkerName,
        string Toolname);
}
