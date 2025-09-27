-- Script tạo cơ sở dữ liệu Apple Store cho DBeaver
-- Chạy script này trong DBeaver để tạo database và xem ERD

-- Tạo bảng AspNetUsers (ApplicationUser)
CREATE TABLE AspNetUsers (
    Id TEXT PRIMARY KEY,
    UserName TEXT,
    NormalizedUserName TEXT,
    Email TEXT,
    NormalizedEmail TEXT,
    EmailConfirmed INTEGER NOT NULL DEFAULT 0,
    PasswordHash TEXT,
    SecurityStamp TEXT,
    ConcurrencyStamp TEXT,
    PhoneNumber TEXT,
    PhoneNumberConfirmed INTEGER NOT NULL DEFAULT 0,
    TwoFactorEnabled INTEGER NOT NULL DEFAULT 0,
    LockoutEnd TEXT,
    LockoutEnabled INTEGER NOT NULL DEFAULT 0,
    AccessFailedCount INTEGER NOT NULL DEFAULT 0,
    FullName TEXT,
    CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Tạo bảng Categories
CREATE TABLE Categories (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Description TEXT,
    ImageUrl TEXT,
    CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP
);

-- Tạo bảng Products
CREATE TABLE Products (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT NOT NULL,
    Description TEXT,
    Price DECIMAL(18,2) NOT NULL,
    OriginalPrice DECIMAL(18,2),
    ImageUrl TEXT,
    ImageUrls TEXT, -- JSON array
    Stock INTEGER NOT NULL DEFAULT 0,
    IsActive INTEGER NOT NULL DEFAULT 1,
    IsFeatured INTEGER NOT NULL DEFAULT 0,
    CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
    CategoryId INTEGER NOT NULL,
    Specifications TEXT,
    Color TEXT,
    Storage TEXT,
    FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
);

-- Tạo bảng CartItems
CREATE TABLE CartItems (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    ProductId INTEGER NOT NULL,
    Quantity INTEGER NOT NULL DEFAULT 1,
    CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- Tạo bảng Orders
CREATE TABLE Orders (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    UserId TEXT NOT NULL,
    TotalAmount DECIMAL(18,2) NOT NULL,
    Status INTEGER NOT NULL DEFAULT 0, -- 0=Pending, 1=Confirmed, 2=Processing, 3=Shipped, 4=Delivered, 5=Cancelled
    ShippingAddress TEXT NOT NULL,
    CustomerName TEXT NOT NULL,
    PhoneNumber TEXT NOT NULL,
    CreatedAt TEXT NOT NULL DEFAULT CURRENT_TIMESTAMP,
    UpdatedAt TEXT,
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id)
);

-- Tạo bảng OrderItems
CREATE TABLE OrderItems (
    Id INTEGER PRIMARY KEY AUTOINCREMENT,
    OrderId INTEGER NOT NULL,
    ProductId INTEGER NOT NULL,
    Quantity INTEGER NOT NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    TotalPrice DECIMAL(18,2) NOT NULL,
    FOREIGN KEY (OrderId) REFERENCES Orders(Id),
    FOREIGN KEY (ProductId) REFERENCES Products(Id)
);

-- Tạo bảng AspNetRoles
CREATE TABLE AspNetRoles (
    Id TEXT PRIMARY KEY,
    Name TEXT,
    NormalizedName TEXT,
    ConcurrencyStamp TEXT
);

-- Tạo bảng AspNetUserRoles
CREATE TABLE AspNetUserRoles (
    UserId TEXT NOT NULL,
    RoleId TEXT NOT NULL,
    PRIMARY KEY (UserId, RoleId),
    FOREIGN KEY (UserId) REFERENCES AspNetUsers(Id),
    FOREIGN KEY (RoleId) REFERENCES AspNetRoles(Id)
);

-- Insert dữ liệu mẫu Categories
INSERT INTO Categories (Id, Name, Description, ImageUrl, CreatedAt) VALUES
(1, 'iPhone', 'Điện thoại thông minh iPhone', '/images/categories/iphone.jpg', '2024-01-01 00:00:00'),
(2, 'MacBook', 'Laptop MacBook', '/images/categories/macbook.jpg', '2024-01-01 00:00:00'),
(3, 'iPad', 'Máy tính bảng iPad', '/images/categories/ipad.jpg', '2024-01-01 00:00:00'),
(4, 'Apple Watch', 'Đồng hồ thông minh Apple Watch', '/images/categories/apple-watch.jpg', '2024-01-01 00:00:00'),
(5, 'AirPods', 'Tai nghe không dây AirPods', '/images/categories/airpods.jpg', '2024-01-01 00:00:00');

-- Insert dữ liệu mẫu Products
INSERT INTO Products (Id, Name, Description, Price, OriginalPrice, ImageUrl, Stock, IsActive, IsFeatured, CategoryId, Specifications, Color, Storage, CreatedAt) VALUES
(1, 'iPhone 15 Pro Max', 'iPhone 15 Pro Max với chip A17 Pro, camera 48MP và khung Titanium cao cấp', 34999000, 36999000, '/images/products/iphone-15-pro-max.jpg', 50, 1, 1, 1, 'Chip A17 Pro, Camera 48MP, Pin 29 giờ, Khung Titanium', 'Titanium Tự nhiên', '256GB', '2024-01-01 00:00:00'),
(2, 'iPhone 15 Pro', 'iPhone 15 Pro với chip A17 Pro và camera Pro 48MP', 29999000, NULL, '/images/products/iphone-15-pro.jpg', 45, 1, 1, 1, 'Chip A17 Pro, Camera 48MP, Pin 23 giờ', 'Titanium Xanh', '128GB', '2024-01-02 00:00:00'),
(3, 'MacBook Air M3', 'MacBook Air với chip M3 siêu mạnh mẽ', 27999000, NULL, '/images/products/macbook-air-m3.jpg', 30, 1, 1, 2, 'Chip M3, 8GB RAM, 256GB SSD', 'Bạc', '256GB', '2024-01-03 00:00:00'),
(4, 'iPad Pro', 'iPad Pro với chip M4 và màn hình Liquid Retina XDR', 24999000, NULL, '/images/products/ipad-pro.jpg', 20, 1, 1, 3, 'Chip M4, 8GB RAM, 256GB', 'Bạc', '256GB', '2024-01-04 00:00:00'),
(5, 'AirPods Pro', 'AirPods Pro với chống ồn chủ động', 6999000, NULL, '/images/products/airpods-pro.jpg', 50, 1, 1, 5, 'Chip H2, Chống ồn chủ động, Pin 30 giờ', NULL, NULL, '2024-01-05 00:00:00');

-- Insert Admin Role
INSERT INTO AspNetRoles (Id, Name, NormalizedName, ConcurrencyStamp) VALUES
('admin-role-id', 'Admin', 'ADMIN', 'admin-stamp');

-- Insert Admin User
INSERT INTO AspNetUsers (Id, UserName, NormalizedUserName, Email, NormalizedEmail, EmailConfirmed, PasswordHash, SecurityStamp, ConcurrencyStamp, FullName, CreatedAt) VALUES
('admin-user-id', 'admin@applestore.com', 'ADMIN@APPLESTORE.COM', 'admin@applestore.com', 'ADMIN@APPLESTORE.COM', 1, 'AQAAAAEAACcQAAAAEHashedPasswordHere', 'security-stamp', 'concurrency-stamp', 'Administrator', '2024-01-01 00:00:00');

-- Assign Admin Role to Admin User
INSERT INTO AspNetUserRoles (UserId, RoleId) VALUES
('admin-user-id', 'admin-role-id');

-- Tạo indexes để tối ưu hiệu năng
CREATE INDEX IX_Products_CategoryId ON Products(CategoryId);
CREATE INDEX IX_CartItems_UserId ON CartItems(UserId);
CREATE INDEX IX_CartItems_ProductId ON CartItems(ProductId);
CREATE INDEX IX_Orders_UserId ON Orders(UserId);
CREATE INDEX IX_OrderItems_OrderId ON OrderItems(OrderId);
CREATE INDEX IX_OrderItems_ProductId ON OrderItems(ProductId);
CREATE INDEX IX_AspNetUserRoles_RoleId ON AspNetUserRoles(RoleId);