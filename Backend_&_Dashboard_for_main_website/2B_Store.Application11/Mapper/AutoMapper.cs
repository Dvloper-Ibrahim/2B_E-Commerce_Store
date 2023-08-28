using _2B_Store.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Mapper
{
    public class AutoMapper : Profile
    {
        public AutoMapper() {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            //CreateMap<CategoryDTO, Category>()
            //    .ForMember(dest => dest.Image,
            //    opt => opt.MapFrom(src => SaveImageAsync(src.Image)));
            CreateMap<SubCategory, SubCategoryDTO>().ReverseMap();
            CreateMap<Product, ProductDTO>().ReverseMap();
            CreateMap<CreateUpdateProductDTO, Product>().ReverseMap();
            CreateMap<ProductImage, ProductImageDTO>().ReverseMap();
            CreateMap<Review, ReviewDTO>().ReverseMap();
            CreateMap<LocationStore, LocationStoreDTO>().ReverseMap();
            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();
            CreateMap<Payment, PaymentDTO>().ReverseMap();
            CreateMap<Shipping, ShippingDTO>().ReverseMap();
            CreateMap<ApplicationUser, UserDTO>().ReverseMap();
            CreateMap<UserSignUpDto, ApplicationUser>().ReverseMap();
            CreateMap<GetAllUsersDTO, ApplicationUser>().ReverseMap();
            CreateMap<UserProfileDto, ApplicationUser>().ReverseMap();
            CreateMap<UserProfileDto, UserSignUpDto>().ReverseMap();
            //CreateMap<IFormFile, string>().ReverseMap();




            // CreateMap<CreateUpdateCategoryDTO, Category>().ReverseMap();



            //CreateMap<Product, GetAllProdsDTO>().ReverseMap();
            //CreateMap<Product, Create_updateProdDTO>().ReverseMap();



        }
        private async Task<string> SaveImageAsync(IFormFile image)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

            string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "categories", uniqueFileName);
            using (var stream = new FileStream(imagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/images/categories/" + uniqueFileName;
        }

    }
}
