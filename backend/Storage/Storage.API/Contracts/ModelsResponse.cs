namespace Storage.API.Contracts
{
    public record ModelsResponse(
        Guid Id,
        string Name,
        Guid CategoryId,
        string CategoryName);
}
