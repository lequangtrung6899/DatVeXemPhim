## 🔐 Về đăng nhập & mật khẩu (đã sửa)

Mật khẩu giờ được băm thật bằng PBKDF2-HMACSHA256 (`Services/PasswordHasherHelper.cs`, dùng
`Rfc2898DeriveBytes` có sẵn trong .NET — không cần thêm gói NuGet). Toàn bộ tài khoản seed sẵn
(2 tài khoản `User` + 3 tài khoản `Customer`) dùng chung mật khẩu demo **`123456`**.

## 🛒 Giỏ hàng, Thanh toán QR ngân hàng & Duyệt hoàn tiền 2 cấp

Ba phần này tái sử dụng tối đa schema sẵn có:

1. **Giỏ hàng** (`CartController`, `/gio-hang`): Chọn ghế xong (`BookingController.Confirm`) sẽ
   tạo một `Ticket` ở trạng thái `"Chờ thanh toán"` — đây chính là một món trong giỏ hàng, **không**
   tạo `Payment` và **không** áp voucher ngay. Trong trang Giỏ hàng, khách có thể nhập mã hoặc chọn
   nhanh từ danh sách voucher đang hoạt động; việc áp dụng gọi AJAX tới
   `POST /gio-hang/ap-dung-voucher` và cập nhật tổng tiền **ngay lập tức** (xem `Services/CartPricing.cs`),
   thay vì phải đợi thanh toán xong mới biết như luồng cũ. Giỏ hàng bị bỏ quên quá 20 phút sẽ tự
   động bị hủy để nhả ghế lại (`CartPricing.ExpireStaleCartAsync`).

2. **Thanh toán QR ngân hàng** (`PaymentController`, `/thanh-toan`): Trang thanh toán hiển thị mã
   QR chuẩn **VietQR** (dùng dịch vụ công khai `img.vietqr.io`, không cần API key) đã **nhúng sẵn số
   tiền cần thanh toán** trong ảnh — khi khách quét bằng app ngân hàng, số tiền sẽ tự động được điền,
   không cần gõ lại. Thông tin ngân hàng (tên NH, số TK, chủ TK) cấu hình trong `appsettings.json`
   (mục `"Bank"`) — **nhớ đổi thành tài khoản thật** nếu dùng cho mục đích khác ngoài demo. Vẫn như
   luồng cũ, đây là **thanh toán mô phỏng** — không có đối soát giao dịch ngân hàng thật, khách tự
   bấm "Tôi đã chuyển khoản — Xác nhận thanh toán" để hoàn tất.

3. **Hoàn tiền cần cả Nhân viên và Admin duyệt** (`Services/RefundService.cs`,
   `AdminRefundController`, `/quan-tri/hoan-tien`): Khách hàng (hoặc Nhân viên hỗ trợ) chỉ có thể
   **gửi yêu cầu** hoàn tiền, không hủy vé/hoàn tiền ngay lập tức nữa. Yêu cầu (`RefundRequest`)
   phải qua đủ 2 bước: Nhân viên duyệt trước → Admin duyệt lần cuối thì vé mới thực sự bị hủy,
   ghế mới được giải phóng và `Payment` mới được đánh dấu `"Đã hoàn tiền"`. Tài khoản vai trò Admin
   có thể hoàn tất cả 2 bước trong 1 lần bấm (Admin có đủ thẩm quyền của cả 2 cấp), còn tài khoản
   Staff chỉ thực hiện được bước đầu. Đây là cơ chế duyệt **chuyên biệt cho hoàn tiền/hủy vé**,
   tách riêng khỏi hàng đợi `PendingChange` dùng cho các trang quản lý khác (xem mục kế tiếp).

## ✅ Duyệt thay đổi cho các thao tác quản lý khác (`PendingChange`)

Ngoài hoàn tiền, các thao tác quản lý còn lại mà tài khoản vai trò **Staff** thực hiện (thêm/sửa/xóa
Combo, Thể loại, Voucher, Phòng chiếu, Suất chiếu, cập nhật trạng thái Thanh toán, khóa/mở khóa tài
khoản Khách hàng) đều đi qua một hàng đợi chờ **Admin** duyệt chung, thay vì áp dụng ngay — chống lạm
quyền. Cơ chế này nằm trong `AdminBaseController.SubmitPendingChangeAsync`, ghi một bản ghi
`PendingChange` (model ở `Models/PendingChange.cs`, JSON hoá dữ liệu thay đổi vào `ChangesJson`), và
được duyệt/áp dụng vào dữ liệu thật qua `AdminApprovalController` (`/quan-tri/cho-duyet`). Tài khoản
vai trò **Admin** vẫn thao tác trực tiếp (không qua hàng đợi) — chỉ Staff mới bị chặn lại để chờ duyệt.
Quản lý **Ghế** (`AdminRoomController` — GenerateGrid/AddSeat/DeleteSeat) và **Phim** (đã có cơ chế
duyệt riêng qua `Movie.ApprovalStatus`/`HasPendingEdit` từ trước) không đi qua `PendingChange`.

**Về migration:** Vì môi trường sinh code này không có sẵn `dotnet`/`dotnet-ef` để chạy
`dotnet ef migrations add`, các migration cho bảng `RefundRequests` (từ nhánh Giỏ hàng/Hoàn tiền)
và bảng `PendingChanges` (từ nhánh Duyệt thay đổi) đã được **viết tay** cẩn thận theo đúng quy ước
EF Core sinh ra (đối chiếu với migration `initialcreate` có sẵn), gộp vào một migration
`AddRefundRequestsAndPendingChanges`, và `ApplicationDbContextModelSnapshot.cs` đã được cập nhật
tương ứng để phản ánh **cả hai** bảng mới. Vì chưa chạy qua `dotnet build`/`dotnet ef` thật để xác
nhận, **nên chạy thử `dotnet build` rồi `dotnet run` (hoặc `dotnet ef database update`) trên máy có
cài .NET SDK trước khi nộp báo cáo** — nếu EF Core báo lỗi liên quan migration này, cách khắc phục
nhanh nhất là xóa migration `AddRefundRequestsAndPendingChanges` và chạy lại
`dotnet ef migrations add AddRefundRequestsAndPendingChanges` để EF tự sinh lại từ model
(`Models/RefundRequest.cs` và `Models/PendingChange.cs` đã sẵn sàng, không cần sửa).

## ⚠️ Về quy trình thanh toán (dành cho phần báo cáo)

Chức năng thanh toán trong dự án (`PaymentController.Confirm`, sau bước Giỏ hàng) là **thanh toán
mô phỏng**, phục vụ mục đích học tập/báo cáo — **không** đối soát với cổng thanh toán/ngân hàng
thật nào. Trang thanh toán hiển thị mã QR chuẩn VietQR có nhúng sẵn số tiền (quét app ngân hàng sẽ
tự động điền đúng số tiền), nhưng khi khách bấm "Tôi đã chuyển khoản — Xác nhận thanh toán", hệ
thống tự động đánh dấu (các) vé là "Đã thanh toán" và tạo bản ghi `Payment` với mã giao dịch có
tiền tố `DH...`, không có giao dịch tiền thật nào được thực hiện. Nếu báo cáo có mục mô tả "quy
trình thanh toán", cần ghi rõ đây là mô phỏng, không phải tích hợp cổng thanh toán/đối soát ngân
hàng thực tế.

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
