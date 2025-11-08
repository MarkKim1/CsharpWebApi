var builder = WebApplication.CreateBuilder();
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.WriteIndented = true;
});
var app = builder.Build();
app.MapControllers();


app.Run();