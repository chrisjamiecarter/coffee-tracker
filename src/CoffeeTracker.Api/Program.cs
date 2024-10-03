using CoffeeTracker.Api.Installers;

namespace CoffeeTracker.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddApi();
        builder.Services.AddDatabase(builder.Configuration);

        var app = builder.Build();
        app.AddMiddleware();
        app.Run();
    }
}
