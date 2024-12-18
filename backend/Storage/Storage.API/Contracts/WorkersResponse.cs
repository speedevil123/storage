namespace Storage.API.Contracts
{
    public record WorkersResponse(
        Guid Id,
        string Name,
        string Position,
        string Email,
        string PhoneNumber,
        DateTime RegistrationDate,
        Guid DepartmentId,
        string DepartmentName);
}
