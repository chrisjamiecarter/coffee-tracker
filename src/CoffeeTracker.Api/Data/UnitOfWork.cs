using CoffeeTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Api.Data;

public class UnitOfWork : IUnitOfWork
{
    #region Fields

    private readonly CoffeeTrackerDataContext _dataContext;

    #endregion
    #region Constructors

    public UnitOfWork(CoffeeTrackerDataContext dataContext, ICoffeeTrackerRepository repository)
    {
        _dataContext = dataContext;
        CoffeeRecord = repository;
    }

    #endregion
    #region Properties

    public ICoffeeTrackerRepository CoffeeRecord { get; set; }

    #endregion
    #region Methods

    public async Task<int> SaveAsync()
    {
        try
        {
            return await _dataContext.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            throw;
        }
    }

    #endregion
}
