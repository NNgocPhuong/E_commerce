using E_Commerce.Data;
using E_Commerce.Helpers;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class CartController : Controller
    {
        private readonly Hshop2023Context _db;

        public CartController(Hshop2023Context context) { 
            _db = context;
        }

        
        public List<CartItem> Cart => HttpContext.Session.Get<List<CartItem>>(MySetting.CARTKEY) ?? new List<CartItem>();
        public IActionResult Index()
        {
            return View(Cart);
        }

        public IActionResult AddToCart(int id, int quantity = 1)
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                var hangHoa = _db.HangHoas.Find(id);
                if (hangHoa == null)
                {
                    TempData["Message"] = "Không tìm thấy hàng hóa";
                    return Redirect("/404");
                }
                item = new CartItem
                {
                    MaHh = hangHoa.MaHh,
                    TenHh = hangHoa.TenHh,
                    Hinh = hangHoa.Hinh ?? "",
                    DonGia = hangHoa.DonGia ?? 0,
                    SoLuong = quantity
                };
                gioHang.Add(item);
            }
            else
            {
                item.SoLuong += quantity;
            }
            HttpContext.Session.Set(MySetting.CARTKEY, gioHang);
            return RedirectToAction("Index", "HangHoa");
        }
        public IActionResult Remove(int id) 
        {
            var gioHang = Cart;
            var item = gioHang.SingleOrDefault(p => p.MaHh == id);
            if (item != null)
            {
                gioHang.Remove(item);
                HttpContext.Session.Set(MySetting.CARTKEY, gioHang);
            }
            return RedirectToAction("Index");

        }
    }
}
