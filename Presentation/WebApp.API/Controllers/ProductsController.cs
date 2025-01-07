using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using WebApp.Application.Services.Facad;
using WebApp.Infrastructure.Comon;
using WebApp.Infrastructure.DTOs;

namespace WebApp.API.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductsController : ControllerBase
  {
    private readonly IProductService ProductService;

    public ProductsController(IProductService productService)
    {
      ProductService = productService;
    }

    [HttpGet]
    public IActionResult Get(/*[FromQuery] Request<ProductRequest> Value*/)
    {
      ResultList<ProductDto> r = ProductService.GetList.Execute(new());
      return Ok(r);
    }

    // GET api/<ProductsController>/5
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
      Request<ProductRequest> Value = new();
      Value.Id = id;
      return Ok(ProductService.Get.Execute(Value));
    }

    [HttpPost]
    public IActionResult Post([FromBody] Request<ProductRequest, ProductDto> Value)
    {
      return Ok(ProductService.Create.Execute(Value));
    }

    [HttpPut("{id}")]
    public IActionResult Put(int id, [FromBody] Request<ProductRequest, ProductDto> Value)
    {
      return Ok(ProductService.Edit.Execute(Value));
    }

    // DELETE api/<ProductsController>/5
    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      Request<ProductRequest, ProductDto> Value = new();
      Value.Data.Id = id;
      return Ok(ProductService.Delete.Execute(Value));
    }
  }
}
