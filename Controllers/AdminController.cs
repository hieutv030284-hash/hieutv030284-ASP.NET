using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppleStoreWeb.Data;
using AppleStoreWeb.Models;

namespace AppleStoreWeb.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public AdminController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var stats = new AdminDashboardViewModel
            {
                TotalProducts = await _context.Products.CountAsync(),
                TotalOrders = await _context.Orders.CountAsync(),
                TotalUsers = await _userManager.Users.CountAsync(),
                TotalRevenue = await _context.Orders.SumAsync(o => o.TotalAmount),
                RecentOrders = await _context.Orders
                    .Include(o => o.User)
                    .OrderByDescending(o => o.CreatedAt)
                    .Take(5)
                    .ToListAsync()
            };
            return View(stats);
        }

        public async Task<IActionResult> Products()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Orders()
        {
            var orders = await _context.Orders
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.Product)
                .OrderByDescending(o => o.CreatedAt)
                .ToListAsync();
            return View(orders);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                order.UpdatedAt = DateTime.Now;
                await _context.SaveChangesAsync();
                TempData["Success"] = "Cập nhật trạng thái đơn hàng thành công!";
            }
            return RedirectToAction("Orders");
        }

        [HttpPost]
        public async Task<IActionResult> ToggleProductStatus(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product != null)
            {
                product.IsActive = !product.IsActive;
                await _context.SaveChangesAsync();
                TempData["Success"] = $"Sản phẩm đã được {(product.IsActive ? "kích hoạt" : "vô hiệu hóa")}!";
            }
            return RedirectToAction("Products");
        }

        public async Task<IActionResult> Users()
        {
            var users = await _userManager.Users
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();
            return View(users);
        }
    }
}