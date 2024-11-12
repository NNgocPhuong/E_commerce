using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class RegisterVM
    {
        [DisplayName("Tên đăng nhập")]
        [Required(ErrorMessage = "Mã khách hàng không được để trống")]
        [MaxLength(20, ErrorMessage = "Mã khách hàng không được quá 20 ký tự")]
        public string MaKh { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        [DataType(DataType.Password)]
        [DisplayName("Mật khẩu")]
        public string MatKhau { get; set; }
        [MaxLength(50, ErrorMessage = "Họ tên không được quá 50 ký tự")]
        [DisplayName("Họ tên")]
        public string HoTen { get; set; }

        public bool GioiTinh { get; set; } = true;

        public DateTime NgaySinh { get; set; }
        [MaxLength(60, ErrorMessage = "Địa chỉ không được quá 60 ký tự")]
        [DisplayName("Địa chỉ")]
        public string DiaChi { get; set; }
        [Phone]
        [MaxLength(24, ErrorMessage = "Điện thoại không được quá 24 ký tự")]
        [RegularExpression(@"(09|03[2|6|8|9])+([0-9]{8})\b", ErrorMessage = "Điện thoại không đúng định dạng")]
        [DisplayName("Điện thoại")]
        public string DienThoai { get; set; }
        [EmailAddress(ErrorMessage = "Email không đúng định dạng")]
        [DisplayName("Email")]
        public string Email { get; set; }

        public string? Hinh { get; set; }
    }
}
