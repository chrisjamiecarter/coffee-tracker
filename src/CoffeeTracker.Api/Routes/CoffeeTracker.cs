﻿using Asp.Versioning.Conventions;
using CoffeeTracker.Api.Contracts.V1;
using CoffeeTracker.Api.Models;
using CoffeeTracker.Api.Services;

namespace CoffeeTracker.Api.Routes;

public static class CoffeeTracker
{
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
            .WithName(nameof(GetAllCoffees))
            .MapToApiVersion(1);

        builder.MapGet("/{id}", GetCoffee)
            .WithName(nameof(GetCoffee))
            .MapToApiVersion(1);

        builder.MapPost("/", CreateCoffee)
            .WithName(nameof(CreateCoffee))
            .MapToApiVersion(1);

        builder.MapPut("/{id}", UpdateCoffee)
            .WithName(nameof(UpdateCoffee))
            .MapToApiVersion(1);

        builder.MapDelete("/{id}", DeleteCoffee)
            .WithName(nameof(DeleteCoffee))
            .MapToApiVersion(1);

        return app;
    }

    private static async Task<IResult> GetAllCoffees(ICoffeeTrackerService service)
    {
        var entities = await service.ReturnAsync();
        return TypedResults.Ok(entities.Select(x => x.ToResponse()));
    }

    private static async Task<IResult> GetCoffee(Guid id, ICoffeeTrackerService service)
    {
        var entity = await service.ReturnAsync(id);

        return entity is null
            ? TypedResults.NotFound()
            : TypedResults.Ok(entity.ToResponse());
    }

    private static async Task<IResult> CreateCoffee(CoffeeRecordRequest request, ICoffeeTrackerService service)
    {
        var model = request.ToModel();
        
        var created = await service.CreateAsync(model);

        return created
            ? TypedResults.CreatedAtRoute(model.ToResponse(), nameof(GetCoffee), new { id = model.Id })
            : TypedResults.BadRequest(new { error = "Unable to create coffee." });
    }

    private static async Task<IResult> UpdateCoffee(Guid id, CoffeeRecordRequest request, ICoffeeTrackerService service)
    {
        var entity = await service.ReturnAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var updatedCoffeeRecord = new CoffeeRecord
        {
            Id = entity.Id,
            Name = request.Name,
            Date = request.Date,
        };

        var updated = await service.UpdateAsync(updatedCoffeeRecord);

        return updated
            ? TypedResults.Ok(updatedCoffeeRecord.ToResponse())
            : TypedResults.BadRequest(new { error = "Unable to update coffee." });
    }

    private static async Task<IResult> DeleteCoffee(Guid id, ICoffeeTrackerService service)
    {
        var entity = await service.ReturnAsync(id);
        if (entity is null)
        {
            return TypedResults.NotFound();
        }

        var deleted = await service.DeleteAsync(entity);

        return deleted
            ? TypedResults.NoContent()
            : TypedResults.BadRequest(new { error = "Unable to delete coffee." });
    }
}
