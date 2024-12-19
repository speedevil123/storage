namespace Storage.API.Contracts
{
    public record ManufacturersRequest(
        Guid Id,
        string Name,
        string phoneNumber,
        string Email,
        string Country,
        string PostIndex);
}
