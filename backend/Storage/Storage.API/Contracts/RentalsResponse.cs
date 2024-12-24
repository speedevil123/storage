﻿namespace Storage.API.Contracts
{
    public record RentalsResponse(
        Guid workerId,
        Guid toolId,
        string workerName,
        string toolName,
        string startDate,
        string returnDate,
        string endDate,
        string status,
        int toolQuantity);
}
