
namespace CoffeeTracker.Api.Data;

public interface IUnitOfWork
{
    ICoffeeTrackerRepository CoffeeRecord { get; set; }

    Task<int> SaveAsync();
}