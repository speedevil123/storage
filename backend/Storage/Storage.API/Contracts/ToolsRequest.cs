namespace Storage.API.Contracts
{
    public record ToolsRequest(
        Guid Id,
        string Type,
        string Model,
        string Manufacturer,
        int Quantity,
        bool isTaken);
}
