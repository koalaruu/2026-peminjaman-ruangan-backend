using Microsoft.EntityFrameworkCore;
using RoomBookingApp.api.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Membaca Connection String
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 2. Registrasi DbContext dengan Pengabaian Peringatan Migrasi (Biar tidak error lagi)
builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseSqlite(connectionString)
           .ConfigureWarnings(w => w.Ignore(Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.PendingModelChangesWarning)));

// 3. Konfigurasi CORS (PENTING: Agar Frontend React bisa akses API)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReactApp",
        policy => policy.WithOrigins("http://localhost:5173", "http://localhost:5174") // Vite dev may pick different port
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 4. Konfigurasi Pipeline HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Gunakan CORS sebelum Authorization dan MapControllers
app.UseCors("AllowReactApp"); 

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();