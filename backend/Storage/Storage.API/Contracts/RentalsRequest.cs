namespace Storage.API.Contracts
{
    public record RentalsRequest(
        Guid WorkerId,
        Guid ToolId,
        DateTime StartDate,
        DateTime ReturnDate,
        DateTime EndDate,
        string Status,
        int ToolQuantity);
}
