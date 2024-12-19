namespace Storage.API.Contracts
{
    public record ManufacturersResponse(
        Guid Id,
        string Name,
        string phoneNumber,
        string Email,
        string Country,
        string PostIndex);
}
