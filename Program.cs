var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.Use(async (context, next) =>
{
    DateTime start = DateTime.UtcNow;
    await next.Invoke();
    var end = DateTime.UtcNow - start;
    System.Console.WriteLine($"Execution time: {end}");
});


app.UseSwagger();
app.UseSwaggerUI();

app.Use(async (context, next) =>
{
    var isAuthenticated = context.Request.Query["authenticated"] == "true";
    if (!isAuthenticated)
    {
        context.Response.StatusCode = StatusCodes.Status403Forbidden;
        if (!context.Response.HasStarted)
        {
            await context.Response.WriteAsync("Access denied: authenticated = false");
            return;
        }
    }
    await next();
});

app.Use(async (context, next) =>
{
    if (context.Request.Method == HttpMethods.Put || context.Request.Method == HttpMethods.Post)
    {
        var isAuthorized = context.Request.Query["authorized"] == "true";
        if (!isAuthorized)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            if (!context.Response.HasStarted)
            {
                await context.Response.WriteAsync("Access denied: only authrized personal can update the data");
                return;
            }
        }
    }
    await next();
});

app.MapControllers();

app.MapFallback(() => Results.NotFound("Sorry we couldn't find that page"));
app.Run();