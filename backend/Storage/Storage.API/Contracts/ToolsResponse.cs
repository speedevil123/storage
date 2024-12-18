namespace Storage.API.Contracts
{
    public record ToolsResponse(
        Guid Id,
        Guid CategoryId,
        Guid ModelId,
        Guid ManufacturerId,
        int Quantity,
        string CategoryName,
        string ModelName,
        string ManufacturerName);
}
