namespace Storage.API.Contracts
{
    public record ToolsResponse(
        Guid Id,
        string Type,
        string Model,
        string Manufacturer,
        int Quantity,
        bool isTaken);
}
