namespace Storage.API.Contracts
{
    public record ToolsResponse(
        Guid Id,
        string Name,
        string Model,
        string Manufacturer,
        int Stock);
}
