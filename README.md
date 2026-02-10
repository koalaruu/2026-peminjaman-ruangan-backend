# 2026-peminjaman-ruangan-backend

## Deskripsi
Repositori ini berisi kode sumber Backend untuk Sistem Peminjaman Ruangan Kampus. Dikembangkan menggunakan ASP.NET Core Web API sebagai bagian dari tugas PdBL 2026.

## Fitur Utama
- Pencatatan Peminjaman Ruangan (CRUD)
- Pengelolaan Status Peminjaman (Pending, Approved, Rejected)
- Riwayat Peminjaman

## Tech Stack
- **Framework:** .NET 8 / ASP.NET Core Web API
- **Database:** MySQL / SQL Server (via Entity Framework Core)
- **Tools:** Postman/Swagger untuk pengujian API

## Cara Menjalankan
1. Clone repositori ini.
2. Buat file `.env` berdasarkan `.env.example` dan sesuaikan kredensial database.
3. Jalankan perintah `dotnet restore`.
4. Jalankan perintah `dotnet ef database update` untuk migrasi database.
5. Jalankan aplikasi dengan `dotnet run`.