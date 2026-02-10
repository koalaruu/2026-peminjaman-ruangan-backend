using Microsoft.EntityFrameworkCore;
using RoomBookingApp.api.Data;

var builder = WebApplication.CreateBuilder(args);

// Membaca Connection String dari appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Registrasi DbContext untuk SQLite
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlite(connectionString));

// Registrasi Controller
builder.Services.AddControllers();

// Konfigurasi Swagger/OpenAPI
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Konfigurasi Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();