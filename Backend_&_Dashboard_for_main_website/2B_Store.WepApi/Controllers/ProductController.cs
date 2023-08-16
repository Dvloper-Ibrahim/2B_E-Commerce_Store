using _2B_Store.Application.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace _2B_Store.WepApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductServices _productService;

        public ProductController(IProductServices productService)
        {
            _productService = productService;
        }

        // GET: api/<ProductController>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAllProducts();
            return Ok(products);
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetProductById(id);
            return Ok(product);
        }

        // GET api/<ProductController>/Category/5
        [HttpGet("category/{id}")]
        public async Task<IActionResult> GetByCategoryId(int id)
        {
            var products = await _productService.GetProductsByCategory(id);
            return Ok(products);
        }

        // GET api/<ProductController>/SubCategory/5
        //[HttpGet("subCategory/{id}")]
        //public async Task<IActionResult> GetBySubCategoryId(int id)
        //{
        //    var products = await _productService.GetProductsBySubCategory(id);
        //    return Ok(products);
        //}

        // GET api/<ProductController>/parentSubCategory/5
        [HttpGet("parentSubCategory/{id}")]
        public async Task<IActionResult> GetByParentSubCatId(int id)
        {
            var products = await _productService.GetProductsByParentSubCat(id);
            return Ok(products);
        }

        // GET api/<ProductController>/childSubCategory/5
        [HttpGet("childSubCategory/{id}")]
        public async Task<IActionResult> GetByChildSubCatId(int id)
        {
            var products = await _productService.GetProductsByChildSubCat(id);
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchProducts(string query)
        {
            var products = await _productService.SearchProducts(query);
            return Ok(products);
        }
        // POST api/<ProductController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<ProductController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<ProductController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
