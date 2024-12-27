namespace Storage.API.Contracts
{
    public record ModelsRequest(
        Guid Id,
        string Name,
        Guid CategoryId
        );
}
