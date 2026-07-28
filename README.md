# DatVeXemPhim — Ghi chú Code First (EF Core Migrations)

Dự án đã được chuyển từ **Database First** (chạy tay `Database/DatVeXemPhim.sql`) sang
**Code First**: schema database và dữ liệu mẫu giờ được định nghĩa hoàn toàn trong code C#
(`Models/*.cs` + `Data/ApplicationDbContext.cs`), và EF Core Migrations sẽ tự sinh ra database
từ đó.

`Database/DatVeXemPhim.sql` vẫn còn trong repo nhưng **chỉ để tham khảo lịch sử** — không cần
chạy file này nữa.

## Cách tạo database lần đầu (máy dev)

Cần cài `dotnet-ef` (chỉ cần làm 1 lần trên máy):

```bash
dotnet tool install --global dotnet-ef
```

Sau đó, tại thư mục gốc project (`DatVeXemPhim.csproj`):

```bash
# 1. Sinh migration đầu tiên từ model hiện tại
dotnet ef migrations add InitialCreate

# 2. Tạo/migrate database (LocalDB, theo connection string trong appsettings.json)
dotnet ef database update
```

Bước 2 cũng tự động chạy mỗi khi bạn `dotnet run` — `Program.cs` đã được cấu hình gọi
`db.Database.Migrate()` khi ứng dụng khởi động, nên **chỉ cần bước 1 một lần**, các lần sau
mở project lên là DB tự cập nhật theo migration mới nhất.

## Khi thay đổi Models (thêm bảng / thêm cột / đổi kiểu dữ liệu...)

```bash
dotnet ef migrations add TenMoTaThayDoi
dotnet run   # hoặc: dotnet ef database update
```

## Dữ liệu mẫu (seed data)

Toàn bộ dữ liệu mẫu trước đây nằm trong các câu `INSERT` của file `.sql` giờ đã chuyển thành
`HasData(...)` trong `Data/ApplicationDbContext.cs` (mục `OnModelCreating`), bao gồm:

- 2 vai trò (Admin, Staff) + 2 tài khoản nhân viên demo (`admin01`, `staff01`)
- 7 thể loại phim, 35 phim (kèm gán thể loại), 2 phòng chiếu + toàn bộ ghế của 2 phòng
- 2 combo, 2 voucher, 3 khách hàng demo

Vé, suất chiếu, thanh toán, đánh giá... không seed sẵn (giống file `.sql` gốc) — bạn tự tạo
suất chiếu ở khu vực quản trị (`/quan-tri/suat-chieu`) rồi đặt vé thử ở phía khách hàng.

## Nếu bạn đã có database cũ tạo từ file `.sql`

EF Core Migrations không biết database đó "đã khớp" với model — nếu muốn dùng lại DB cũ thay
vì tạo mới, có 2 lựa chọn:

1. **Đơn giản nhất — xóa và tạo lại theo Code First** (mất dữ liệu cũ, nhưng có sẵn seed data
   tương đương): xóa database cũ rồi chạy `dotnet ef database update`.
2. **Giữ dữ liệu cũ:** sau khi chạy `dotnet ef migrations add InitialCreate`, đánh dấu migration
   này là "đã áp dụng" mà không chạy lại DDL, bằng cách chèn thủ công 1 dòng vào bảng
   `__EFMigrationsHistory` của DB cũ (migration id + `ProductVersion` khớp EF Core 6.0.25) thay
   vì chạy `dotnet ef database update`.
