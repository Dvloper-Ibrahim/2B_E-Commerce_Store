using _2B_Store.Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace _2B_Store.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryServices _categoryService;
        //private readonly IConfiguration _configuration;

        public CategoryController(ICategoryServices categoryService/*, IConfiguration configuration*/)
        {
            _categoryService = categoryService;
            //_configuration = configuration;
        }

        // GET: api/<CategoryController>
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var Cats = await _categoryService.GetAllCategories();
            return Ok(Cats);
        }

        // GET api/<CategoryController>/5
        //[Route("gategorybyid/{id}")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCatbyId(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            { return NotFound("Category is Not found"); }
            else

                return Ok(category);
        }

        //// POST api/<CategoryController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/<CategoryController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/<CategoryController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
