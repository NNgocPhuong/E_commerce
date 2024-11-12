using AutoMapper;
using E_Commerce.Data;
using E_Commerce.ViewModels;

namespace E_Commerce.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<RegisterVM, KhachHang>(); // Map RegisterVM to KhachHang
        }
    }
}
