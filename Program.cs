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

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();


app.Run();