using CoffeeTracker.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace CoffeeTracker.Api.Data;

public class CoffeeTrackerDataContext : DbContext
{
    #region Constructors

    public CoffeeTrackerDataContext(DbContextOptions<CoffeeTrackerDataContext> options) : base(options) { }

    #endregion
    #region Properties

    public DbSet<CoffeeRecord> CoffeeRecord { get; set; } = default!;

    #endregion
}
