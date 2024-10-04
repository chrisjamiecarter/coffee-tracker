using CoffeeTracker.Api.Models;

namespace CoffeeTracker.Api.Data;
public interface ICoffeeTrackerRepository
{
    Task CreateAsync(CoffeeRecord coffeeRecord);
    Task DeleteAsync(CoffeeRecord coffeeRecord);
    Task<IEnumerable<CoffeeRecord>> ReturnAsync();
    Task<CoffeeRecord?> ReturnAsync(Guid id);
    Task UpdateAsync(CoffeeRecord coffeeRecord);
}