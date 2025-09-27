# Các sửa đổi đã thực hiện cho AppleStore

## Vấn đề đã sửa:

### 1. Lỗi trang Cart (http://localhost:5044/Cart)
**Vấn đề**: Trang Cart không hiển thị được do thiếu include Category
**Giải pháp**: 
- Sửa `CartController.Index()` để include Category: `.ThenInclude(p => p.Category)`
- Sửa `CheckoutController.Index()` và `PlaceOrder()` tương tự

### 2. Chức năng quản lý thông tin cá nhân
**Vấn đề**: Chưa có trang quản lý thông tin cá nhân người dùng
**Giải pháp**:
- Tạo `ProfileController.cs` với các action:
  - `Index()`: Hiển thị thông tin cá nhân
  - `UpdateProfile()`: Cập nhật thông tin
  - `ChangePassword()`: Đổi mật khẩu
- Tạo `ProfileViewModel.cs` và `ChangePasswordViewModel.cs`
- Tạo view `Views/Profile/Index.cshtml`
- Cập nhật navigation trong `_Layout.cshtml`

### 3. Chức năng quản lý đơn hàng
**Bổ sung**:
- Tạo `OrdersController.cs` với các action:
  - `Index()`: Danh sách đơn hàng
  - `Details()`: Chi tiết đơn hàng
- Tạo views `Views/Orders/Index.cshtml` và `Views/Orders/Details.cshtml`
- Cập nhật navigation để link đến trang đơn hàng

### 4. Sửa lỗi Checkout view
**Vấn đề**: Hàm `GetProductSlug()` không cần thiết và gây lỗi
**Giải pháp**: 
- Thay thế bằng sử dụng `ImageUrl` trực tiếp
- Loại bỏ hàm `@functions`

## Các file đã tạo mới:
- `Controllers/ProfileController.cs`
- `Controllers/OrdersController.cs`
- `Models/ProfileViewModel.cs`
- `Views/Profile/Index.cshtml`
- `Views/Orders/Index.cshtml`
- `Views/Orders/Details.cshtml`

## Các file đã sửa đổi:
- `Controllers/CartController.cs`
- `Controllers/CheckoutController.cs`
- `Views/Checkout/Index.cshtml`
- `Views/Shared/_Layout.cshtml`

## Cách sử dụng:

### Trang Cart:
- Truy cập: http://localhost:5044/Cart
- Chức năng: Xem, cập nhật số lượng, xóa sản phẩm trong giỏ hàng
- Thanh toán: Click nút "Thanh toán" để chuyển đến trang checkout

### Trang Profile:
- Truy cập: Click vào tên user > "Thông tin cá nhân"
- Chức năng: 
  - Cập nhật họ tên, số điện thoại
  - Đổi mật khẩu
  - Xem đơn hàng

### Trang Orders:
- Truy cập: Click vào tên user > "Đơn hàng của tôi"
- Chức năng:
  - Xem danh sách đơn hàng
  - Xem chi tiết từng đơn hàng
  - Theo dõi trạng thái đơn hàng

## Lưu ý:
- Cần đăng nhập để sử dụng các chức năng Cart, Profile, Orders
- Tất cả các controller đã được bảo vệ bằng `[Authorize]`
- Database relationships đã được cấu hình đúng với Include/ThenInclude