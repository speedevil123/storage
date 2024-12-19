namespace Storage.API.Contracts
{
    public record DepartmentsResponse(
        Guid Id,
        string Name,
        string phoneNumber,
        string Email,
        string Address);
}
