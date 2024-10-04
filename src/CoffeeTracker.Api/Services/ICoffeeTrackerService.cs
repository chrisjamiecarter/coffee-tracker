using CoffeeTracker.Api.Models;

namespace CoffeeTracker.Api.Services;
public interface ICoffeeTrackerService
{
    Task<bool> CreateAsync(CoffeeRecord coffeeRecord);
    Task<bool> DeleteAsync(CoffeeRecord coffeeRecord);
    Task<IEnumerable<CoffeeRecord>> ReturnAsync();
    Task<CoffeeRecord?> ReturnAsync(Guid id);
    Task<bool> UpdateAsync(CoffeeRecord coffeeRecord);
}