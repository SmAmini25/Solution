using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Entity;
using WebApp.Domain.Interface;
using WebApp.Infrastructure.Comon;
using WebApp.Infrastructure.DTOs;

namespace WebApp.Application.Services.Edit
{
  public interface IProductEditService
  {
    Result<ProductDto> Execute(Request<ProductRequest, ProductDto> Value);
  }

  public class ProductEditService : IProductEditService
  {
    private readonly IDBContext DB;

    public ProductEditService(IDBContext dB)
    {
      DB = dB;
    }

    public Result<ProductDto> Execute(Request<ProductRequest, ProductDto> Value)
    {
      Result<ProductDto> result = new();

      Product? product = DB.Product.Where(x => x.IsRemoved == false).FirstOrDefault(x => x.Id == Value.Entity.Id);

      if (product == null)
      {
        result.State = State.NotFound;
        return result;
      }
      if (Value.Entity.Name == "1") // چک یک قانون فرضی
      {
        result.State = State.Error;
        result.ErrorMessage = "یک خطا رخ داده";
      }

      if (result.State == State.OK)
      {
        product.Name = Value.Entity.Name;
        product.Price = Value.Entity.Price;
        product.Amount = Value.Entity.Amount;

        DB.SaveChanges();
      }

      result.Entity = Value.Entity;
      return result;
    }
  }
}
