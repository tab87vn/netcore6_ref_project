using NLog;
using tab.TestDotNet.API.Extensions;
using tab.TestDotNet.Services.Contracts;

var builder = WebApplication.CreateBuilder(args);

LogManager.LoadConfiguration(
    string.Concat(Directory.GetCurrentDirectory(),
    "/nlog.config"));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// custom services
builder.Services.ConfigureCors("corsPolicy");
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureUserRepository();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // app.UseDeveloperExceptionPage();
}
else
{
    app.UseHsts(); // strictly transport security
}

// app.UseExceptionHandler();
// app.UseCors("corspolicy");

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
