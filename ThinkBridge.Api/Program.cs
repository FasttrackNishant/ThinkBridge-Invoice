using Microsoft.EntityFrameworkCore;
using ThinkBridge.Api.Infrastructure;
using ThinkBridge.Api.Infrastructure.Services.DatabaseCollection;
using ThinkBridge.Api.Infrastructure.Services.InMemory;
using ThinkBridge.Api.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// I am using SQL Server As DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("InvoiceAppConnectionString")));

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IInMemoryRepository, InvoiceInMemoryRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazor",
        policy =>
        {
            policy.WithOrigins("https://localhost:7024")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ThinkBridge API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowBlazor");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();