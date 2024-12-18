﻿using Storage.Core.Models;

namespace Storage.API.Contracts
{
    public record PenaltiesRequest(
        Guid Id,
        double Fine,
        DateTime PenaltyDate,
        Guid WorkerId,
        Guid ToolId,
        string WorkerName,
        string Toolname);
}
