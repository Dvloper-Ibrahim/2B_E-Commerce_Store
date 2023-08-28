using _2B_Store.Application.Services;
using _2B_Store.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace _2B_Store.MVC.Controllers
{
    [Authorize(Roles = "Admin,Sup_Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryServices _categoryServices;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CategoryController(ICategoryServices categoryServices, IWebHostEnvironment webHostEnvironment)
        {
            _categoryServices = categoryServices;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryServices.GetAllCategories();
            return View(categories);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var category = await _categoryServices.GetCategoryById(id);
            if (category == null)
                return NotFound();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, CategoryDTO categoryDTO, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null && image.Length > 0)
                        categoryDTO.Image = await SaveImageAsync(image);

                    var updatedCategory = await _categoryServices.UpdateCategory(id, categoryDTO);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Image", "The image field is required");
                    //ModelState.AddModelError(string.Empty, "An error occurred while updating the category. Please try again later.");
                }
            }

            return View(categoryDTO);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO categoryDTO, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (image != null && image.Length > 0)
                        categoryDTO.Image = await SaveImageAsync(image);

                    await _categoryServices.AddCategory(categoryDTO);
                    return RedirectToAction("Index");
                }
                catch (Exception)
                {
                    ModelState.AddModelError("Image", "The image field is required");
                }
            }
            return View(categoryDTO);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryServices.DeleteCategory(id);
            return RedirectToAction("Index");
        }

        private async Task<string> SaveImageAsync(IFormFile image)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);

            string dashboardImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "categories", uniqueFileName);
            string apiCategpriesDir = Path.Combine(_webHostEnvironment.WebRootPath, "..", "..",
                "2B_Store.WepApi", "wwwroot", "images", "categories");
            string apiImagePath = Path.Combine(apiCategpriesDir, uniqueFileName);

            using (var stream = new FileStream(dashboardImagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }
            using (var stream = new FileStream(apiImagePath, FileMode.Create))
            {
                await image.CopyToAsync(stream);
            }

            return "/images/categories/" + uniqueFileName;
        }
    }
}



