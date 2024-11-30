namespace Storage.API.Contracts
{
    public record ToolsRequest(
        Guid Id,
        string Name,
        string Model,
        string Manufacturer,
        int Stock);
}
