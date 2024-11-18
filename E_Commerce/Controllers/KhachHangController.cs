
using AutoMapper;
using E_Commerce.Data;
using E_Commerce.Helpers;
using E_Commerce.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
        [HttpPost]
        public async Task<IActionResult> DangNhap(LoginVM model, string? returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            if (ModelState.IsValid)
            {
                var khachHang = _db.KhachHangs.SingleOrDefault(kh => kh.MaKh == model.Username);
                if (khachHang == null)
                {
                    ModelState.AddModelError("Username", "Sai thông tin đăng nhập");
                }
                else
                {
                    if (!khachHang.HieuLuc)
                    {
                        ModelState.AddModelError("Username", "Tài khoản đã bị khóa");
                    }
                    else
                    {
                        var matKhau = DataEncryptionExtensions.ToMd5Hash(model.Password, khachHang.RandomKey);
                        if (khachHang.MatKhau != matKhau)
                        {
                            ModelState.AddModelError("Password", "Sai thông tin đăng nhập");
                        }
                        else
                        {
                            var claims = new List<Claim>
                            {
                                new Claim(ClaimTypes.Email, khachHang.Email),
                                new Claim(ClaimTypes.Name, khachHang.HoTen),
                                new Claim("CustomerId", khachHang.MaKh),
                                new Claim(ClaimTypes.Role, "Customer")
                            };
                            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
                            await HttpContext.SignInAsync(claimsPrincipal);
                            if (string.IsNullOrEmpty(returnUrl))
                            {
                                return RedirectToAction("Index", "HangHoa");
                            }
                            return Redirect(returnUrl);
                        }
                    }
                }
            }
            return View(model);
        }
        #endregion
        [Authorize]
        public IActionResult Profile()
        {
            return View();
        }
        [Authorize]
        public async Task<IActionResult> DangXuat()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "HangHoa");
        }
    }
}
