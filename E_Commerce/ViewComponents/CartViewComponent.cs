using E_Commerce.Helpers;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {

        public IViewComponentResult Invoke()
        {
            var cart = HttpContext.Session.Get<List<CartItem>>(MySetting.CARTKEY) ?? new List<CartItem>();
            return View("CartPanel", new CartModel { Quantity = cart.Sum(p => p.SoLuong), Total = cart.Sum(p => p.ThanhTien)});
        }
    }
}
