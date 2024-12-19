namespace Storage.API.Contracts
{
    public record DepartmentsRequest(
        Guid Id,
        string Name,
        string phoneNumber,
        string Email,
        string Address);
}
