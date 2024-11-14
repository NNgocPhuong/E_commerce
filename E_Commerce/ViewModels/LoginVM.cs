using System.ComponentModel.DataAnnotations;

namespace E_Commerce.ViewModels
{
    public class LoginVM
    {
        [Required(ErrorMessage = "*")]
        [Display(Name = "Tên đăng nhập")]
        [MaxLength(20, ErrorMessage = "Không quá 20 ký tự")]
        public string Username { get; set; }
        [DataType(DataType.Password)]
        [Required(ErrorMessage = "*")]
        [Display(Name = "Mật khẩu")]
        public string Password { get; set; }
    }
}
