using E_Commerce.Data;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.ViewComponents
{
    public class MenuLoaiViewComponent : ViewComponent
    {
        private readonly Hshop2023Context db;

        public MenuLoaiViewComponent(Hshop2023Context context)
        {
            db = context;
        }
        public IViewComponentResult Invoke()
        {
            var items = db.Loais.Select(lo =>
            new MenuLoaiVM {
               MaLoai = lo.MaLoai, TenLoai = lo.TenLoai, SoLuong = lo.HangHoas.Count
            }).OrderBy(p => p.TenLoai);
            return View(items);
        }
    }
}
