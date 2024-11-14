
using AutoMapper;
using E_Commerce.Data;
using E_Commerce.Helpers;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Controllers
{
    public class KhachHangController : Controller
    {
        private readonly Hshop2023Context _db;
        private readonly IMapper _mapper;

        public KhachHangController(Hshop2023Context context, IMapper mapper) {
            _db = context;
            _mapper = mapper;
        }
        #region DangKy
        [HttpGet]
        public IActionResult DangKy()
        {
            var model = new RegisterVM();
            return View(model);
        }
        [HttpPost]
        public IActionResult DangKy(RegisterVM model, IFormFile Hinh)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }
            if (ModelState.IsValid)
            {
                var khachHang = _mapper.Map<KhachHang>(model);
                khachHang.RandomKey = MyUtil.GenarateRandomKey();
                khachHang.MatKhau = DataEncryptionExtensions.ToMd5Hash(model.MatKhau, khachHang.RandomKey);
                khachHang.HieuLuc = true;
                khachHang.VaiTro = 0;

                if(Hinh != null)
                {

                    khachHang.Hinh = MyUtil.UploadHinh("KhachHangNew", Hinh);
                }
                _db.KhachHangs.Add(khachHang);
                _db.SaveChanges();
                return RedirectToAction("Index", "HangHoa");
            }
            return View(model);
        }
        #endregion

        #region DangNhap
        [HttpGet]
        public IActionResult DangNhap(string? returnUrl)
        {
            var model = new LoginVM();
            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }
        #endregion
    }
}
