using _2B_Store.Application11.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;

namespace _2B_Store.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubCategoryController : ControllerBase
    {
        private readonly ISubCategoryServices _subCategoryServices;
        //private readonly IConfiguration _configuration;

        public SubCategoryController(ISubCategoryServices categoryService/*, IConfiguration configuration*/)
        {
            _subCategoryServices = categoryService;
            //_configuration = configuration;
        }

        //GET: api/<SubCategoryController>
        //[HttpGet]
        //public async Task<IActionResult> GetAllSubCategories()
        //{
        //    var SubCats = await _subCategoryServices.GetAllSubCategories();
        //    return Ok(SubCats);
        //}

        // GET All Subcategory
        //[HttpGet]
        //public async Task<IActionResult> GetAllSubCategories()
        //{
        //    var SubCats = await _subCategoryServices.GetAllSubCategories();

        //    var options = new JsonSerializerOptions
        //    {
        //        ReferenceHandler = ReferenceHandler.Preserve,
        //        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        //    };
        //    string jsonString = JsonSerializer.Serialize(SubCats, options);
        //    return Ok(jsonString);
        //}

        // GET GET Subcategory byId api/<SubCategoryController>/5
        //[Route("GetSubCatbyId")]
        [HttpGet("{id}")]
        //[Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetSubCatbyId(int id)
        {
            var Subcategory = await _subCategoryServices.GetSubCategoryById(id);
            if (Subcategory == null)
            { return NotFound("SubCategory is Not found"); }
            else
                return Ok(Subcategory);
        }

        //get Parent Subcats by CategoryId
        [HttpGet("parent_in_category/{id}")]
        public async Task<IActionResult> GetParentSubCatbycategoryId(int id)
        {
            var subcategories = await _subCategoryServices.GetParentSubCategsBy_CategId(id);
            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //};
            //string jsonString = JsonSerializer.Serialize(subcategories, options);
            //return Ok(jsonString);
            return Ok(subcategories);
        }

        //get Child Subcats in Parent SubCat
        [HttpGet("child_to_subCategory/{id}")]
        public async Task<IActionResult> GetChildSubCatbyParentSubId(int id)
        {
            var subcategories = await _subCategoryServices.GetChildSubCatby_ParentSubId(id);
            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve,
            //    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            //};
            //string jsonString = JsonSerializer.Serialize(subcategories, options);
            //return Ok(jsonString);
            return Ok(subcategories);
        }
    }
}
