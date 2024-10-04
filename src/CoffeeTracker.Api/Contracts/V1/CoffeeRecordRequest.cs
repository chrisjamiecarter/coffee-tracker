namespace CoffeeTracker.Api.Contracts.V1;

public class CoffeeRecordRequest
{
    #region Properties

    public required string Name { get; set; }

    public required DateTime Date { get; set; }

    #endregion
}
