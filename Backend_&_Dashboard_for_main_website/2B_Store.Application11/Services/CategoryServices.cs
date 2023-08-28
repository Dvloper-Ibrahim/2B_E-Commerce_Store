using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2B_Store.Application.Contracts;
using _2B_Store.DTO;
using AutoMapper;
using Microsoft.AspNetCore.Http;

namespace _2B_Store.Application.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;
        public CategoryServices(ICategoryRepository categoryRepository , IMapper mapper)
        {

            _categoryRepository = categoryRepository;
            _mapper = mapper;

        }

        public async Task<List<CategoryDTO> > GetAllCategories()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<List<CategoryDTO>>(categories);

        }


        public async Task<CategoryDTO> GetCategoryById(int categoryId)
        {
            var category = await _categoryRepository.GetByIdAsync(categoryId);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<CategoryDTO> AddCategory(CategoryDTO categoryDTO)
        {
            var category = _mapper.Map<Category>(categoryDTO);
            //category.Image = await SaveImageAsync(categoryDTO.Image);
            /*category = */await _categoryRepository.AddAsync(category);
            //await _categoryRepository.SaveChangesAsync();
            //return _mapper.Map<CategoryDTO>(category);
            return categoryDTO;
        }

        public async Task<CategoryDTO> UpdateCategory(int categoryId, CategoryDTO categoryDTO)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
            if (existingCategory == null)
                throw new ArgumentException("Category not found");

            _mapper.Map(categoryDTO, existingCategory);
            existingCategory = await _categoryRepository.UpdateAsync(existingCategory);
            //await _categoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryDTO>(existingCategory);
        }

        public async Task DeleteCategory(int categoryId)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(categoryId);
            if (existingCategory == null)
                throw new ArgumentException("Category not found");

            await _categoryRepository.DeleteAsync(existingCategory);
            //await _categoryRepository.SaveChangesAsync();
        }

        //private async Task<string> SaveImageAsync(IFormFile image)
        //{
        //    string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

        //    string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "categories", uniqueFileName);
        //    using (var stream = new FileStream(imagePath, FileMode.Create))
        //    {
        //        await image.CopyToAsync(stream);
        //    }

        //    return "/images/categories/" + uniqueFileName;
        //}












        #region All Categories
        //All Categories
        //public async Task<List<GetAllCategsDTO>> GetCategoryallPagination(int Item, int pagenumber)
        //{
        //    var AllCat = await _ICategoryRepository.GetAllAsync();
        //    var ItemPagination = AllCat.Skip(Item * (pagenumber - 1)).Take(Item).
        //        Select(c => new GetAllCategsDTO
        //        {
        //            Id = c.ID,
        //            Name = c.Name,
        //            Type = c.Type,
        //            Image = c.Image,
        //            SubCategories = c.SubCategories,


        //        }).ToList();

        //    return ItemPagination;
        //}
        #endregion

        #region Category By ID
        //Category By ID


        //public async Task<CategoryDTO> GetCategoryById(int Categoryid)
        //{
        //    var catbyID = await _ICategoryRepository.GetByIdAsync(Categoryid);

        //    return _mapper.Map<CategoryDTO>(catbyID);

        //    #region handle Map
        //    //var categoryDTO = new GetAllCategsDTO
        //    //{
        //    //    Id = catbyID.ID,
        //    //    NameEN = catbyID.NameEN,
        //    //    NameAR = catbyID.NameAR,

        //    //    Image = catbyID.Image,
        //    //    SubCategories = catbyID.SubCategories,
        //    //};

        //    //return categoryDTO;
        //    #endregion

        //}
        #endregion

        #region Find Category
        //Find Category
        //public async Task<List<GetAllCategsDTO>> FindCategories(string searchTerm, int? minSubCategories, int? maxSubCategories)
        //{
        //    var categories = _ICategoryRepository.FindAsync();

        //    if (!string.IsNullOrEmpty(searchTerm))
        //    {
        //        categories = categories.Where(c => c.Name.Contains(searchTerm));
        //    }

        //    if (minSubCategories.HasValue)
        //    {
        //        categories = categories.Where(c => c.SubCategories.Count >= minSubCategories.Value);
        //    }

        //    if (maxSubCategories.HasValue)
        //    {
        //        categories = categories.Where(c => c.SubCategories.Count <= maxSubCategories.Value);
        //    }

        //    var categoryDTOs = await categories
        //        .Select(c => new GetAllCategsDTO
        //        {
        //            Id = c.Id,
        //            Name = c.Name,
        //            Image = c.Image,
        //            SubCategories = c.SubCategories,
        //            Description = c.Description


        //        })
        //        .ToListAsync();

        //    return categoryDTOs;
        //}
        #endregion


        #region Add Category
        //Add Category

        //public async Task<Create_updateCategDTO> AddCategory(Create_updateCategDTO categoryDTO)
        //{
        //    Category category = new Category();

        //    category.ID = categoryDTO.Id;
        //    category.Name = categoryDTO.Name;
        //    category.Image = categoryDTO.Image;
        //    category.SubCategories = categoryDTO.SubCategories;
        //    category.Type = categoryDTO.Type;
        //    category.Description = categoryDTO.Description;

        //    await _ICategoryRepository.AddAsync(category);
        //    await _ICategoryRepository.savechangesAsync();

        //    return new Create_updateCategDTO
        //    {
        //        Id = category.ID,
        //        Name = category.Name,
        //        Image = category.Image
        //        ,
        //        SubCategories = category.SubCategories,
        //        Type = category.Type,
        //        Description = categoryDTO.Description
        //    };
        //}
        #endregion

        //Update category
        #region Update category
        //public async Task<Create_updateCategDTO> UpdateCategory(int categoryId, Create_updateCategDTO categoryDTO)
        //{
        //    Category category = await _ICategoryRepository.GetByIdAsync(categoryId);

        //    if (category == null)
        //    {
        //        throw new Exception("Category not found");
        //    }

        //    category.Name = categoryDTO.Name;
        //    category.Image = categoryDTO.Image;
        //    category.SubCategories = categoryDTO.SubCategories;
        //    category.Type = categoryDTO.Type;
        //    category.Description = categoryDTO.Description;

        //    await _ICategoryRepository.UpdateAsync(category);
        //    await _ICategoryRepository.savechangesAsync();

        //    return new Create_updateCategDTO
        //    {
        //        Id = category.ID,
        //        Name = category.Name,
        //        Image = category.Image,
        //        SubCategories = category.SubCategories,
        //        Type = category.Type,
        //        Description = category.Description
        //    };
        //}
        #endregion

        //    public Category GetCategoryByName(Category name)
        //    {
        //        throw new NotImplementedException();
        //    }
    }
}
