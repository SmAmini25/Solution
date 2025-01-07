using Grpc.Core;
using WebApp.Application.Services.Facad;
using WebApp.Infrastructure.Comon;
using WebApp.Infrastructure.DTOs;

namespace WebApp.Grpc.Services
{
  public class ProductsService : ProductServiceGRpc.ProductServiceGRpcBase
  {
    private readonly IProductService ProductService;

    public ProductsService(IProductService productService)
    {
      ProductService = productService;
    }


    public override async Task Create(IAsyncStreamReader<ProductGRequestDto> requestStream, IServerStreamWriter<ProductGDto> responseStream, ServerCallContext context)
    {
      await foreach (var item in requestStream.ReadAllAsync())
      {
        Request<ProductRequest, ProductDto> request = new();
        request.Entity.Name = item.Name;
        request.Entity.Price = item.Price;
        request.Entity.Amount = item.Amount;
        var result = ProductService.Create.Execute(request);

        ProductGDto product = new ProductGDto();
        product.Id = result.Entity.Id;
        product.Name = result.Entity.Name;
        product.Price = result.Entity.Price ?? 0;
        product.Amount = result.Entity.Amount ?? 0;
        await responseStream.WriteAsync(product);
      }
    }

    public override async Task<ProductGDto> Delete(IAsyncStreamReader<ProductGRequestDto> requestStream, ServerCallContext context)
    {

      await foreach (var item in requestStream.ReadAllAsync())
      {
        Request<ProductRequest, ProductDto> request = new();
        request.Entity.Name = item.Name;
        request.Entity.Price = item.Price;
        request.Entity.Amount = item.Amount;
        var result = ProductService.Create.Execute(request);

        var product = await base.Delete(requestStream, context);
        product.Id = result.Entity.Id;
        product.Name = result.Entity.Name;
        product.Price = result.Entity.Price ?? 0;
        product.Amount = result.Entity.Amount ?? 0;
        return product;
      }


      return await base.Delete(requestStream, context);
    }

    public override async Task<ProductGDto> Get(ProductGRequestDto request, ServerCallContext context)
    {
      Request<ProductRequest> R = new();
      R.Id = request.Id;
      var result = ProductService.Get.Execute(R);

      var product = new ProductGDto();
      product.Id = result.Entity.Id;
      product.Name = result.Entity.Name;
      product.Price = result.Entity.Price ?? 0;
      product.Amount = result.Entity.Amount ?? 0;
      return product;
    }

    public override Task GetList(ProductGRequestDto request, IServerStreamWriter<ProductGDto> responseStream, ServerCallContext context)
    {
      Request<ProductRequest> R = new();
      R.Date.Name = request.Name;
      var result = ProductService.GetList.Execute(R);

      foreach (var item in result.Entity)
      {
        var product = new ProductGDto();
        product.Id = item.Id;
        product.Name = item.Name;
        product.Price = item.Price ?? 0;
        product.Amount = item.Amount ?? 0;
        responseStream.WriteAsync(product);
      }

      return Task.CompletedTask;

      //return   base.GetList(request, responseStream, context);
    }

    public override async Task<ProductGDto> UpDate(ProductGRequestDto request, ServerCallContext context)
    {
      Request<ProductRequest, ProductDto> R = new();
      R.Entity.Id = request.Id;
      R.Entity.Name = request.Name;
      R.Entity.Price = request.Price;
      R.Entity.Amount = request.Amount;
      var result = ProductService.Edit.Execute(R);


      var product = new ProductGDto();
      if (result.State == Infrastructure.Comon.State.OK)
      {
        product.Id = result.Entity.Id;
        product.Name = result.Entity.Name;
        product.Price = result.Entity.Price ?? 0;
        product.Amount = result.Entity.Amount ?? 0;
        //product.ErrorMessage = result.ErrorMessage;
      }
      else
      {
        product.ErrorMessage = result.ErrorMessage;
        product.State = State.Error;
      }
      return product;
    }
  }
}
