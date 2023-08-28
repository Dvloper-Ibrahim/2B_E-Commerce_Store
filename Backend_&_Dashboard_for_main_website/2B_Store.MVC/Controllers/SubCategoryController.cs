using _2B_Store.Application.Services;
using _2B_Store.Application11.Services;
using _2B_Store.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace _2B_Store.MVC.Controllers
{
    [Authorize(Roles = "Admin,Sup_Admin")]
    public class SubCategoryController : Controller
    {
        private readonly ISubCategoryServices _subCategoryServices;
        private readonly ICategoryServices _categoryServices;

        public SubCategoryController(
            ISubCategoryServices subCategoryServices,
            ICategoryServices categoryServices
            )
        {
            _subCategoryServices = subCategoryServices;
            _categoryServices = categoryServices;
        }

        public async Task<IActionResult> Index()
        {
            var subCategories = await _subCategoryServices.GetAllSubCategories();
            return View(subCategories);
        }

        public async Task<IActionResult> Create()
        {
            //var categories = await _categoryServices.GetAllCategories();
            //ViewBag.Categories = categories;
            var subCategory = new SubCategoryDTO();
            subCategory.Categories = await _categoryServices.GetAllCategories();
            subCategory.SubCategories = await _subCategoryServices.GetAllSubCategories();
            return View(subCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SubCategoryDTO subCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _subCategoryServices.AddSubCategory(subCategoryDTO);
                return RedirectToAction("Index");
            }

            //subCategoryDTO.Categories = await _categoryServices.GetAllCategories();
            //subCategoryDTO.SubCategories = await _subCategoryServices.GetAllSubCategories();
            //ViewBag.Categories = categories;
            return View(subCategoryDTO);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var subCategory = await _subCategoryServices.GetSubCategoryById(id);
            if (subCategory == null)
                return NotFound();

            //var categories = await _categoryServices.GetAllCategories();
            //ViewBag.Categories = categories;
            subCategory.Categories = await _categoryServices.GetAllCategories();
            subCategory.SubCategories = await _subCategoryServices.GetAllSubCategories();
            return View(subCategory);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, SubCategoryDTO subCategoryDTO)
        {
            if (ModelState.IsValid)
            {
                await _subCategoryServices.UpdateSubCategory(id, subCategoryDTO);
                return RedirectToAction("Index");
            }
            //var categories = await _categoryServices.GetAllCategories();
            //ViewBag.Categories = categories;
            return View(subCategoryDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _subCategoryServices.DeleteSubCategory(id);
            return RedirectToAction("Index");
        }

        //public IActionResult getSubCategories(int categoryId)
        //{
        //    var categories = _subCategoryServices.GetSubCategsBy_CategId(categoryId);
        //    return Json(categories);
        //}
    }
}
