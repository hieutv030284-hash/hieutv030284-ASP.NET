using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppleStoreWeb.Data;
using AppleStoreWeb.Models;

namespace AppleStoreWeb.Controllers
{
    [Authorize]
    public class CheckoutController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CheckoutController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .ThenInclude(p => p.Category)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return RedirectToAction("Index", "Cart");
            }

            return View(cartItems);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrder()
        {
            var userId = _userManager.GetUserId(User);
            var cartItems = await _context.CartItems
                .Include(c => c.Product)
                .ThenInclude(p => p.Category)
                .Where(c => c.UserId == userId)
                .ToListAsync();

            if (!cartItems.Any())
            {
                return Json(new { success = false, message = "Giỏ hàng trống" });
            }

            var order = new Order
            {
                UserId = userId!,
                TotalAmount = cartItems.Sum(c => c.Product.Price * c.Quantity),
                Status = OrderStatus.Processing,
                CustomerName = "Khách hàng",
                PhoneNumber = "0123456789",
                ShippingAddress = "Địa chỉ giao hàng"
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            foreach (var cartItem in cartItems)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.Price,
                    TotalPrice = cartItem.Product.Price * cartItem.Quantity
                };
                _context.OrderItems.Add(orderItem);
            }

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Đặt hàng thành công!", orderId = order.Id });
        }
    }
}