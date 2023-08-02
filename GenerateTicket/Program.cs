using GenerateTicket.Models;
using GenerateTicket.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Connection string
var connectionString = builder.Configuration.GetConnectionString("DbConnection");

// Register AppDbContext
builder.Services.AddDbContextPool<AppDbContext>(db => db.UseSqlServer(connectionString));

// Register TicketInfo service
builder.Services.AddScoped<ITicketInfoService, TicketInfoService>();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
