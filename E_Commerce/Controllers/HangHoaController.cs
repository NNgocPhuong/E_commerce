using E_Commerce.Data;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Controllers
{
    public class HangHoaController : Controller
    {
        private readonly Hshop2023Context _db;

        public HangHoaController(Hshop2023Context context) { _db = context; }
        public IActionResult Index(int? loai)
        {
            var items = _db.HangHoas.AsQueryable();
            if (loai.HasValue)
            {
                items = items.Where(p => p.MaLoai == loai);
            }
            var result = items.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                Hinh = p.Hinh ?? "",
                DonGia = p.DonGia ?? 0,
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        public IActionResult Search(string? query)
        {
            var items = _db.HangHoas.AsQueryable();
            if (query != null)
            {
                items = items.Where(p => p.TenHh.Contains(query));
            }

            var result = items.Select(p => new HangHoaVM
            {
                MaHh = p.MaHh,
                TenHh = p.TenHh,
                Hinh = p.Hinh ?? "",
                DonGia = p.DonGia ?? 0,
                MoTaNgan = p.MoTaDonVi ?? "",
                TenLoai = p.MaLoaiNavigation.TenLoai
            });
            return View(result);
        }
        public IActionResult Detail(int id)
        {
            var item = _db.HangHoas
                .Include(p => p.MaLoaiNavigation)
                .SingleOrDefault(p => p.MaHh == id);
            if (item == null)
            {
                TempData["Message"] = "Không tìm thấy hàng hóa";
                return Redirect("/404");
            }
            var result = new ChiTietHangHoaVM
            {
                MaHh = item.MaHh,
                TenHh = item.TenHh,
                Hinh = item.Hinh ?? "",
                DonGia = item.DonGia ?? 0,
                MoTaNgan = item.MoTaDonVi ?? "",
                TenLoai = item.MaLoaiNavigation.TenLoai,
                MoTaChiTiet = item.MoTa ?? "",
                DiemDanhGia = 5, //check sau
                SoLuongTon = 10//tinh sau
            };
            return View(result);
        }
    }
}
