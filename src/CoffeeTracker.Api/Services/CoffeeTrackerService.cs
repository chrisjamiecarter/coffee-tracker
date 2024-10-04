﻿using CoffeeTracker.Api.Data;
using CoffeeTracker.Api.Models;

namespace CoffeeTracker.Api.Services;

public class CoffeeTrackerService : ICoffeeTrackerService
{
    #region Fields

    private readonly IUnitOfWork _unitOfWork;

    #endregion
    #region Constructors

    public CoffeeTrackerService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    #endregion
    #region Methods

    public async Task<bool> CreateAsync(CoffeeRecord coffeeRecord)
    {
        await _unitOfWork.CoffeeRecord.CreateAsync(coffeeRecord);
        var created = await _unitOfWork.SaveAsync();
        return created > 0;
    }

    public async Task<bool> DeleteAsync(CoffeeRecord coffeeRecord)
    {
        await _unitOfWork.CoffeeRecord.DeleteAsync(coffeeRecord);
        var deleted = await _unitOfWork.SaveAsync();
        return deleted > 0;
    }

    public async Task<IEnumerable<CoffeeRecord>> ReturnAsync()
    {
        return await _unitOfWork.CoffeeRecord.ReturnAsync();
    }

    public async Task<CoffeeRecord?> ReturnAsync(Guid id)
    {
        return await _unitOfWork.CoffeeRecord.ReturnAsync(id);
    }

    public async Task<bool> UpdateAsync(CoffeeRecord coffeeRecord)
    {
        await _unitOfWork.CoffeeRecord.UpdateAsync(coffeeRecord);
        var updated = await _unitOfWork.SaveAsync();
        return updated > 0;
    }

    #endregion
}
