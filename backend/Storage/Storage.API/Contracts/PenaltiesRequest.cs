using Storage.Core.Models;

namespace Storage.API.Contracts
{
    public record PenaltiesRequest(
        Guid Id,
        double Fine,
        DateTime PenaltyDate,
        bool IsPaidOut,
        Guid RentalId);
}
