using CoffeeTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Api.Data;

public class CoffeeTrackerRepository : ICoffeeTrackerRepository
{
    #region Fields

    private readonly CoffeeTrackerDataContext _dataContext;

    #endregion
    #region Constructors

    public CoffeeTrackerRepository(CoffeeTrackerDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    #endregion
    #region Methods

    public async Task CreateAsync(CoffeeRecord coffeeRecord)
    {
        await _dataContext.CoffeeRecord.AddAsync(coffeeRecord);
    }

    public async Task DeleteAsync(CoffeeRecord coffeeRecord)
    {
        var entity = await _dataContext.CoffeeRecord.FindAsync(coffeeRecord.Id);
        if (entity is not null)
        {
            _dataContext.CoffeeRecord.Remove(entity);
        }
    }

    public async Task<IEnumerable<CoffeeRecord>> ReturnAsync()
    {
        return await _dataContext.CoffeeRecord.OrderBy(o => o.Date).ToListAsync();
    }

    public async Task<CoffeeRecord?> ReturnAsync(Guid id)
    {
        return await _dataContext.CoffeeRecord.FindAsync(id);
    }

    public async Task UpdateAsync(CoffeeRecord coffeeRecord)
    {
        var entity = await _dataContext.CoffeeRecord.FindAsync(coffeeRecord.Id);
        if (entity is not null)
        {
            entity.Name = coffeeRecord.Name;
            entity.Date = coffeeRecord.Date;
            _dataContext.CoffeeRecord.Update(entity);
        }
    }

    #endregion
}
