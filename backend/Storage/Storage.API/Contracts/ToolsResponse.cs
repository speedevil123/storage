namespace Storage.API.Contracts
{
    public record ToolsResponse(
        Guid Id,
        Guid ModelId,
        Guid ManufacturerId,
        int Quantity,
        string ModelName,
        string ManufacturerName);
}
