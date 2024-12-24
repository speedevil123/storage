namespace Storage.API.Contracts
{
    public record RentalsRequest(
        Guid workerId,
        Guid toolId,
        string startDate,
        string returnDate,
        string endDate,
        string status,
        int toolQuantity
        );
}
