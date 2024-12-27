namespace Storage.API.Contracts
{
    public record ToolsRequest(
        Guid Id,
        Guid ModelId,
        Guid ManufacturerId,
        int Quantity);
}
