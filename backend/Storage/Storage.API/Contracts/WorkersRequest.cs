namespace Storage.API.Contracts
{
    public record WorkersRequest(
        Guid Id,
        string Name,
        string Position,
        string Email,
        string PhoneNumber,
        DateTime RegistrationDate,
        Guid DepartmentId,
        string DepartmentName);
}
