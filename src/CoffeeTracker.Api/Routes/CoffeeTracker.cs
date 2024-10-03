using Asp.Versioning.Conventions;
using CoffeeTracker.Api.Contracts.V1;
using CoffeeTracker.Api.Models;

namespace CoffeeTracker.Api.Routes;

public static class CoffeeTracker
{
    private static List<CoffeeRecord> _coffees = [
        new CoffeeRecord
        {
            Id = Guid.NewGuid(),
            Name = "Latte",
            Date = DateTime.Now.AddDays(-1),
        },
        new CoffeeRecord
        {
            Id = Guid.NewGuid(),
            Name = "Cappuccino",
            Date = DateTime.Now.AddDays(2),
        },
        new CoffeeRecord
        {
            Id = Guid.NewGuid(),
            Name = "White Americano",
            Date = DateTime.Now.AddDays(-3),
        },
        new CoffeeRecord
        {
            Id = Guid.NewGuid(),
            Name = "Black Americano",
            Date = DateTime.Now.AddDays(-3),
        },
        ];

    public static readonly string EndpointPrefix = "";

    public static WebApplication MapCoffeeTrackerEndpoints(this WebApplication app)
    {

        var apiVersionSet = app.NewApiVersionSet()
            .HasApiVersion(1)
            .HasApiVersion(2)
            .ReportApiVersions()
            .Build();

        var builder = app.MapGroup("/api/v{version:apiVersion}/coffees")
            .WithApiVersionSet(apiVersionSet);
                
        builder.MapGet("/", GetAllCoffees)
            .MapToApiVersion(1);

        builder.MapGet("/{id}", GetCoffee)
            .MapToApiVersion(1);

        builder.MapPost("/", CreateCoffee)
            .MapToApiVersion(1);

        builder.MapPut("/{id}", UpdateCoffee)
            .MapToApiVersion(1);

        builder.MapDelete("/{id}", DeleteCoffee)
            .MapToApiVersion(1);

        return app;
    }

    private static async Task<IResult> GetAllCoffees()
    {
        return TypedResults.Ok(_coffees.OrderBy(x => x.Date));
    }

    private static async Task<IResult> GetCoffee(Guid id)
    {
        var coffeeRecord = GetCoffeeRecord(id);

        return coffeeRecord is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(new CoffeeRecordResponse
            {
                Id = coffeeRecord.Id,
                Name = coffeeRecord.Name,
                Date = coffeeRecord.Date,
            });
    }

    private static async Task<IResult> CreateCoffee(CoffeeRecordRequest request)
    {
        var coffeeRecord = new CoffeeRecord
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Date = request.Date,
        };

        _coffees.Add(coffeeRecord);

        var response = new CoffeeRecordResponse
        {
            Id = coffeeRecord.Id,
            Name = coffeeRecord.Name,
            Date = coffeeRecord.Date,
        };

        return TypedResults.Created($"{EndpointPrefix}/{response.Id}", response);
    }

    private static async Task<IResult> UpdateCoffee(Guid id, CoffeeRecordRequest request)
    {
        var coffeeRecord = GetCoffeeRecord(id);
        if (coffeeRecord is null)
        {
            return TypedResults.NotFound();
        }
        
        _coffees.Remove(coffeeRecord);

        coffeeRecord.Name = request.Name;
        coffeeRecord.Date = request.Date;

        _coffees.Add(coffeeRecord);

        var response = new CoffeeRecordResponse
        {
            Id = coffeeRecord.Id,
            Name = coffeeRecord.Name,
            Date = coffeeRecord.Date,
        };

        return TypedResults.Ok(response);
    }

    private static async Task<IResult> DeleteCoffee(Guid id)
    {
        var coffeeRecord = GetCoffeeRecord(id);
        if (coffeeRecord is null)
        {
            return TypedResults.NotFound();
        }

        _coffees.Remove(coffeeRecord);

        return TypedResults.NoContent();
    }

    private static CoffeeRecord? GetCoffeeRecord(Guid id)
    {
        return _coffees.First(x => x.Id == id);
    }
}
