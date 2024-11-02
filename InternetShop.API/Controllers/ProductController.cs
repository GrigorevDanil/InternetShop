using InternetShop.Application.Interfaces;
using InternetShop.Application.Products.Commands;
using InternetShop.Application.Products.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InternetShop.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly ICacheService cacheService;

        public ProductController(ICacheService cacheService)
        {
            this.cacheService = cacheService;
        }

        [Authorize]
        [HttpGet]
        public async Task<ActionResult> GetAll([FromServices]GetProductsHandler handler)
        {
            var response = await cacheService.GetOrCreate(
                "AllProducts",
                async () => await handler.Handle());

            return Ok(response);
        }


        
        [HttpPost]
        public async Task<ActionResult> PublishProduct(
        PublishProductHandler handler,
        [FromForm] PublishProductCommand command)
        {
            var result = await handler.Handle(command);

            if (result.IsFailure)
                return BadRequest(result.Error);

            return Ok();    
        }

    }
}
