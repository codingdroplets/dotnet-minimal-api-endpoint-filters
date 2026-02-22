using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var products = new List<Product>
{
    new(1, "Mechanical Keyboard", 79.99m),
    new(2, "Wireless Mouse", 29.99m),
    new(3, "USB-C Hub", 39.99m)
};

app.MapGet("/api/products", () => Results.Ok(products));

app.MapPost("/api/products", ([FromBody] ProductCreateRequest request) =>
{
    var nextId = products.Count == 0 ? 1 : products.Max(p => p.Id) + 1;
    var created = new Product(nextId, request.Name.Trim(), request.Price);
    products.Add(created);
    return Results.Created($"/api/products/{created.Id}", created);
})
.AddEndpointFilter(new ProductValidationFilter());

app.MapGet("/api/health", () => Results.Ok(new
{
    status = "ok",
    machine = Environment.MachineName,
    uptimeSeconds = Math.Round((DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).TotalSeconds)
}));

app.Run();

public record Product(int Id, string Name, decimal Price);
public record ProductCreateRequest(string Name, decimal Price);

public sealed class ProductValidationFilter : IEndpointFilter
{
    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        var request = context.Arguments.OfType<ProductCreateRequest>().FirstOrDefault();
        if (request is null) return Results.BadRequest(new { error = "Request payload is required." });
        if (string.IsNullOrWhiteSpace(request.Name) || request.Name.Trim().Length < 3)
            return Results.BadRequest(new { error = "Name must be at least 3 characters." });
        if (request.Price <= 0 || request.Price > 10_000)
            return Results.BadRequest(new { error = "Price must be greater than 0 and <= 10000." });

        return await next(context);
    }
}
