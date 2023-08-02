using Microsoft.EntityFrameworkCore;
using TicketService.Models;
using TicketService.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DbConnection");
builder.Services.AddDbContextPool<AppDbContext>(db => db.UseSqlServer(connectionString));


// Register Ticket Service
builder.Services.AddScoped<ITicketServices, TicketServices>();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
