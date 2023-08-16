using _2B_Store.Application.Contracts;
using _2B_Store.DTO;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2B_Store.Application11.Services
{
    public class SubCategoryServices : ISubCategoryServices
    {

        private readonly ISubCategoryRepository _subCategoryRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public SubCategoryServices(
            ISubCategoryRepository subCategoryRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper
            )
        {
            _subCategoryRepository = subCategoryRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<List<SubCategoryDTO>> GetAllSubCategories()
        {
            var subCategories = await _subCategoryRepository.GetAllAsync();
            var subCategsDTOs = _mapper.Map<List<SubCategoryDTO>>(subCategories);
            foreach (var item in subCategsDTOs)
            {
                var category = await _categoryRepository.GetByIdAsync(item.CategoryId);
                item.Category = _mapper.Map<CategoryDTO>(category);
                var subCategory = await _subCategoryRepository.GetByIdAsync(Convert.ToInt32(item.SubcategoryId));
                item.Subcategory = _mapper.Map<SubCategoryDTO>(subCategory);
            }
            return subCategsDTOs;
        }

        public async Task<SubCategoryDTO> GetSubCategoryById(int subCategoryId)
        {
            var subCategory = await _subCategoryRepository.GetByIdAsync(subCategoryId);
            return _mapper.Map<SubCategoryDTO>(subCategory);
        }

        public async Task<SubCategoryDTO> AddSubCategory(SubCategoryDTO subCategoryDTO)
        {
            var subCategory = _mapper.Map<SubCategory>(subCategoryDTO);
            subCategory = await _subCategoryRepository.AddAsync(subCategory);
            //await _subCategoryRepository.SaveChangesAsync();
            return _mapper.Map<SubCategoryDTO>(subCategory);
        }

        public async Task<SubCategoryDTO> UpdateSubCategory(int subCategoryId, SubCategoryDTO subCategoryDTO)
        {
            var existingSubCategory = await _subCategoryRepository.GetByIdAsync(subCategoryId);
            if (existingSubCategory == null)
                throw new ArgumentException("SubCategory not found");

            _mapper.Map(subCategoryDTO, existingSubCategory);
            existingSubCategory = await _subCategoryRepository.UpdateAsync(existingSubCategory);
            await _subCategoryRepository.SaveChangesAsync();
            return _mapper.Map<SubCategoryDTO>(existingSubCategory);
        }

        public async Task DeleteSubCategory(int subCategoryId)
        {
            var existingSubCategory = await _subCategoryRepository.GetByIdAsync(subCategoryId);
            if (existingSubCategory == null)
                throw new ArgumentException("SubCategory not found");

            await _subCategoryRepository.DeleteAsync(existingSubCategory);
            //await _subCategoryRepository.SaveChangesAsync();
        }

        public async Task<List<SubCategoryDTO>> GetParentSubCategsBy_CategId(int categoryId)
        {
            var subCategories = (await _subCategoryRepository.GetAllAsync())
                .Where(subcat => subcat.CategoryId == categoryId && subcat.SubcategoryId == null);
            var subCategsDTOs = _mapper.Map<List<SubCategoryDTO>>(subCategories);

            return subCategsDTOs;
        }

        public async Task<List<SubCategoryDTO>> GetChildSubCatby_ParentSubId(int subCatId)
        {
            var subCategories = (await _subCategoryRepository.GetAllAsync())
                .Where(subcat => subcat.SubcategoryId == subCatId);
            var subCategsDTOs = _mapper.Map<List<SubCategoryDTO>>(subCategories);

            return subCategsDTOs;
        }
    }
}
