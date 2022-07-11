var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

var dictionary = new Dictionary<string, int>();

app.MapGet("/wells/{jobId}", (string jobId) =>
{
    var cacheKey = $"WELLS_COUNT_{jobId}";

    if (!dictionary.ContainsKey($"WELLS_COUNT_{jobId}"))
    {
        dictionary.Add(cacheKey, new Random().Next(0, 1000));
        return Results.Ok();
    }

    return Results.Ok(-1);

})
.Produces<int>(StatusCodes.Status200OK);

app.Run();