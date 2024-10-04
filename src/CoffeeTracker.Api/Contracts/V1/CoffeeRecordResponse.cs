using CoffeeTracker.Api.Models;

namespace CoffeeTracker.Api.Contracts.V1;

public class CoffeeRecordResponse
{
    #region Properties

    public required Guid Id { get; set; }

    public required string Name { get; set; }

    public required DateTime Date { get; set; }

    #endregion
}
