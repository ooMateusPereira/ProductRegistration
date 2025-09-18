
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductRegistration.Services;

namespace CadastroProdutos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {

        private IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet]
        public ActionResult<List<Product>> Get()
        {
            return Ok(productsService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<Product> GetById(int id)
        {
            var product = productsService.GetWithId(id);

            if (product is null)
            {
                return NotFound($"Product {id} not found.");
            }

            return Ok(product);
        }

        [HttpPost]
        public ActionResult Post(Product newProduct)
        {
            productsService.AddNewProduct(newProduct);

            return Created();
        }

        [HttpPut ("{id}")]
        public ActionResult<Product> Put(int id, Product uptadedProduct)
        {
            var product = productsService.Update(id, uptadedProduct);

            if (product is null)
            {
                return NotFound($"Product {id} not found.");
            }

            return Ok(product);
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            bool deleted = productsService.Delete(id);

            if (deleted == false)
            {
                return NotFound($"Product {id} not found.");
            }

            return NoContent();
        }

    }
}
