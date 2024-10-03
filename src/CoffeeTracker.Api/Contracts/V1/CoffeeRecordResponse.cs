namespace CoffeeTracker.Api.Contracts.V1;

public class CoffeeRecordResponse
{
    #region Properties

    public Guid Id { get; set; }

    public string Name { get; set; }

    public DateTime Date { get; set; }

    #endregion
}
