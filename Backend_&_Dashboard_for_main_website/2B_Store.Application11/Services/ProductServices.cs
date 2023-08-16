using _2B_Store.Application.Contracts;
using _2B_Store.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application.Services
{
    public class ProductServices : IProductServices
    {

        private readonly IProductRepository _productRepository;
        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IProductImageRepository _productImageRepository;
        private readonly IMapper _mapper;

        public ProductServices(
            IProductRepository productRepository,
            ISubCategoryRepository subCategoryRepository,
            ICategoryRepository categoryRepository,
            IProductImageRepository productImageRepository,
            IMapper mapper
            )
        {
            _productRepository = productRepository;
            _subCategoryRepository = subCategoryRepository;
            _categoryRepository = categoryRepository;
            _productImageRepository = productImageRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductDTO>> GetProductAllPagination(int itemsPerPage, int pageNumber)
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<List<ProductDTO>>(products)
                        .Skip((pageNumber - 1) * itemsPerPage)
                        .Take(itemsPerPage)
                        .ToList();
        }

        public async Task<ProductDTO> GetProductById(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> AddProduct(ProductDTO productDTO)
        {
            //productDTO.Image.ProductId = 
            var product = _mapper.Map<Product>(productDTO);
            product = await _productRepository.AddAsync(product);
            //await _productRepository.SaveChangesAsync();
            var newProduct = _mapper.Map<ProductDTO>(product);
            //newProduct.Image.ImageUrl = productDTO.Image.ImageUrl;
            //newProduct.Image.ProductId = product.Id;

            //var productImage = _mapper.Map<ProductImage>(newProduct.Image);
            //await _productImageRepository.AddAsync(productImage);
            return newProduct;
        }

        public async Task<ProductDTO> UpdateProduct(int productId, ProductDTO productDTO)
        {
            var existingProduct = await _productRepository.GetByIdAsync(productId);
            if (existingProduct == null)
                throw new ArgumentException("Product not found");

            _mapper.Map(productDTO, existingProduct);
            existingProduct = await _productRepository.UpdateAsync(existingProduct);
            var newProduct = _mapper.Map<ProductDTO>(existingProduct);
            //newProduct.Image = productDTO.Image;
            //newProduct.Image.ProductId = existingProduct.Id;

            //var productImage = _mapper.Map<ProductImage>(newProduct.Image);
            //var storedImage = await _productImageRepository
            //    .GetImageByProductData(productImage.ProductId, productImage.ImageUrl);
            //storedImage = await _productImageRepository.UpdateAsync(storedImage);
            //newProduct.Image = _mapper.Map<ProductImageDTO>(storedImage);
            //await _productRepository.SaveChangesAsync();
            return newProduct;
        }

        public async Task DeleteProduct(int productId)
        {
            // Implementation to delete a product
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
                throw new ArgumentException("Product not found");

            await _productRepository.DeleteAsync(product);
            //await _productRepository.SaveChangesAsync();
        }

        public async Task<List<ProductDTO>> GetProductsByCategory(int categoryId)
        {
            var products = await _productRepository.GetProductsByCategory(categoryId);
            return _mapper.Map<List<ProductDTO>>(products).ToList();
        }

        //public async Task<List<ProductDTO>> GetProductsBySubCategory(int subCategoryId)
        //{
        //    var products = await _productRepository.GetProductsBySubCategory(subCategoryId);
        //    return _mapper.Map<List<ProductDTO>>(products).ToList();
        //}

        public async Task<List<ProductDTO>> GetProductsByParentSubCat(int subCategoryId)
        {
            var subCategories = await _subCategoryRepository.GetSubCategoriesByParentSubCat(subCategoryId);
            List<ProductDTO> products = new List<ProductDTO>();
            foreach (var subCat in subCategories)
            {
                var subProducts = await GetProductsByChildSubCat(subCat.Id);
                foreach (var item in subProducts)
                    products.Add(item);
            }
            return products;
            //var products = await _productRepository.GetProductsByParentSubCat(subCategoryId);
            //return _mapper.Map<List<ProductDTO>>(products).ToList();
        }

        public async Task<List<ProductDTO>> GetProductsByChildSubCat(int subCategoryId)
        {
            var products = await _productRepository.GetProductsByChildSubCat(subCategoryId);
            return _mapper.Map<List<ProductDTO>>(products).ToList();
        }

        public async Task<List<ProductDTO>> GetProductsByBrand(string brand)
        {
            var products = await _productRepository.GetProductsByBrand(brand);
            return _mapper.Map<List<ProductDTO>>(products).ToList();
        }

        public async Task<List<ProductDTO>> GetProductsByPriceRange(decimal minPrice, decimal maxPrice)
        {
            var products = await _productRepository.GetProductsByPriceRange(minPrice, maxPrice);
            return _mapper.Map<List<ProductDTO>>(products).ToList();
        }

        public async Task<List<ProductDTO>> GetProductsByStore(string storeName)
        {
            var products = await _productRepository.GetProductsByStore(storeName);
            return _mapper.Map<List<ProductDTO>>(products).ToList();
        }

        public async Task<List<ProductDTO>> GetAllProducts()
        {
            var products = await _productRepository.GetAllAsync();
            var myProducts = _mapper.Map<List<ProductDTO>>(products);
            foreach (var item in myProducts)
            {
                var subCategory = await _subCategoryRepository.GetByIdAsync(item.SubcategoryId);
                item.SubCategory = _mapper.Map<SubCategoryDTO>(subCategory);
                var category = await _categoryRepository.GetByIdAsync(item.SubCategory.CategoryId);
                item.SubCategory.Category = _mapper.Map<CategoryDTO>(category);
            }
            return myProducts;
        }

        public async Task<List<ProductDTO>> SearchProducts(string query)
        {
            query = query.ToLower();
            var products = await _productRepository.GetAllAsync();
            var searchedProducts = await products.Where(p => 
                p.ProductNameEN.ToLower().Contains(query) ||
                p.ProductNameAR.ToLower().Contains(query)).ToListAsync();
            return _mapper.Map<List<ProductDTO>>(searchedProducts);
        }

        ////////////////////////////////////////////////////
        ///
        #region tst

        //public async Task<List<GetAllProdsDTO>> GetProductallPagination(int Item, int pagenumber)
        //{
        //    var products = await _productRepository.GetAllAsync();
        //    var pagedProducts = products.Skip((pagenumber - 1) * Item).Take(Item).ToList();
        //    return _mapper.Map<List<GetAllProdsDTO>>(pagedProducts);
        //}

        //public async Task<GetAllProdsDTO> GetProductById(int productId)
        //{
        //    var product = await _productRepository.GetByIdAsync(productId);
        //    return _mapper.Map<GetAllProdsDTO>(product);
        //}

        //public async Task<Create_updateProdDTO> AddProduct(Create_updateProdDTO productDTO)
        //{
        //    var product = _mapper.Map<Product>(productDTO);
        //    product = await _productRepository.AddAsync(product);
        //    return _mapper.Map<Create_updateProdDTO>(product);
        //}

        //public async Task<Create_updateProdDTO> UpdateProduct(int productId, Create_updateProdDTO productDTO)
        //{
        //    var existingProduct = await _productRepository.GetByIdAsync(productId);
        //    if (existingProduct == null)
        //    {
        //        return null;
        //    }

        //    _mapper.Map(productDTO, existingProduct);
        //    existingProduct = await _productRepository.UpdateAsync(existingProduct);
        //    return _mapper.Map<Create_updateProdDTO>(existingProduct);
        //}

        #endregion

        #region Code without mapping
        //private readonly IProductRepository _IProductRepository;
        //private readonly  IMapper _mapper;
        //public ProductServices(IProductRepository productRepository)
        //{

        //    _IProductRepository = productRepository;
        //}

        ////All Products
        //public async Task<List<GetAllProdsDTO>> GetProductallPagination(int Item, int pagenumber)
        //{
        //    var AllProduct = await _IProductRepository.GetAllAsync();
        //    var ItemPagination = AllProduct.Skip(Item * (pagenumber - 1)).Take(Item).
        //        Select(c => new GetAllProdsDTO
        //        {
        //            Id = c.Id,
        //            ProductName = c.ProductNameEN,
        //            Price = c.Price,
        //            Images = c.Images,
        //            Stock = c.Stock,
        //            Reviews = c.Reviews,
        //            SubCategory = c.SubCategory,
        //            LocationStores = c.LocationStores,



        //        }).ToList();

        //    return ItemPagination;
        //}


        ////Prouct By ID
        //public async Task<GetAllProdsDTO> GetProductById(int Productid)
        //{
        //    var catbyID = await _IProductRepository.GetByIdAsync(Productid);

        //    var ProdsDTO = new GetAllProdsDTO
        //    {
        //        Id = catbyID.Id,
        //        ProductName = catbyID.ProductNameEN,
        //        Price = catbyID.Price,
        //        Images = catbyID.Images,
        //        Stock = catbyID.Stock,
        //        Reviews = catbyID.Reviews,
        //        SubCategory = catbyID.SubCategory,
        //        LocationStores = catbyID.LocationStores,
        //    };

        //    return ProdsDTO;
        //}

        ////Add Product
        //public async Task<Create_updateProdDTO> AddProduct(Create_updateProdDTO productDTO)
        //{
        //    Product product = new Product();


        //    product.ProductNameEN = productDTO.ProductName;
        //    product.Images = productDTO.Images;
        //    product.SubCategory = productDTO.SubCategory;
        //    product.Reviews = productDTO.Reviews;
        //    product.Price = productDTO.Price;
        //    product.Stock = productDTO.Stock;
        //    product.LocationStores = productDTO.LocationStores;


        //    await _IProductRepository.AddAsync(product);
        //    await _IProductRepository.SaveChangesAsync();

        //    return new Create_updateProdDTO
        //    {

        //        ProductName = productDTO.ProductName,
        //        Images = productDTO.Images,
        //        SubCategory = productDTO.SubCategory,
        //        Reviews = productDTO.Reviews,
        //        Price = productDTO.Price,
        //        Stock = productDTO.Stock,
        //        LocationStores = productDTO.LocationStores,

        //    };
        //}


        ////Update Product


        //public async Task<Create_updateProdDTO> UpdateProduct(int productID, Create_updateProdDTO productDTO)
        //{
        //    Product product = await _IProductRepository.GetByIdAsync(productID);

        //    if (product == null)
        //    {
        //        throw new Exception("Category not found");
        //    }

        //    product.ProductNameEN = productDTO.ProductName;
        //    product.Images = productDTO.Images;
        //    product.SubCategory = productDTO.SubCategory;
        //    product.Reviews = productDTO.Reviews;
        //    product.BrandEN = productDTO.Brand;
        //    product.Price = productDTO.Price;
        //    product.Stock = productDTO.Stock;
        //    product.LocationStores = productDTO.LocationStores;

        //    await _IProductRepository.UpdateAsync(product);
        //    await _IProductRepository.SaveChangesAsync();

        //    return new Create_updateProdDTO
        //    {
        //        ProductName = product.ProductNameEN,
        //        Images = product.Images,
        //        SubCategory = product.SubCategory,
        //        Reviews = product.Reviews,
        //        Price = product.Price,
        //        Brand = product.BrandEN,
        //        Stock = product.Stock,
        //        LocationStores = product.LocationStores
        //    };
        //}

        #endregion


    }
}