namespace Storage.API.Contracts
{
    public record WorkersRequest(
        Guid Id,
        string Name,
        string Position,
        string Department,
        string Email,
        string Phone,
        DateTime RegistrationDate
        );
}
