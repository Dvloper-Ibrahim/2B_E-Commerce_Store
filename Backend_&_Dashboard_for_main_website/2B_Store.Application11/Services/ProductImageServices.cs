using _2B_Store.Application.Contracts;
using _2B_Store.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public class ProductImageServices : IProductImageServices
    {

        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ProductImageServices(IProductImageRepository productImageRepository, IMapper mapper)
        {
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductImageDTO>> GetImagesByProductId(int productId)
        {
            var images = await _productImageRepository.GetImagesByProductId(productId);
            return _mapper.Map<IEnumerable<ProductImageDTO>>(images);
        }

        public async Task<ProductImageDTO> AddProductImage(ProductImageDTO productImageDTO)
        {
            var image = _mapper.Map<ProductImage>(productImageDTO);
            image = await _productImageRepository.AddAsync(image);
            //await _productImageRepository.SaveChangesAsync();
            return _mapper.Map<ProductImageDTO>(image);
        }

        public async Task<ProductImageDTO> UpdateMyProductImage(ProductImageDTO productImg)
        {
            var productImage = await _productImageRepository
                .GetImageByProductData(productImg.ProductId, productImg.ImageUrl);
            if (productImage == null)
                throw new ArgumentException("ProductImage not found");
           
            _mapper.Map(productImg, productImage);
            productImage = await _productImageRepository.UpdateAsync(productImage);
            //await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductImageDTO>(productImage);
        }
        public async Task DeleteProductImage(int imageId)
        {
            var image = await _productImageRepository.GetByIdAsync(imageId);
            if (image != null)
            {
                await _productImageRepository.DeleteAsync(image);
                await _productImageRepository.SaveChangesAsync();
            }
        }

    }
}
