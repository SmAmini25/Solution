using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Entity;
using WebApp.Domain.Interface;
using WebApp.Infrastructure.Comon;
using WebApp.Infrastructure.DTOs;

namespace WebApp.Application.Services.Delete
{
  public interface IProductDeleteService
    {
        Result<ProductDto> Execute(Request<ProductRequest, ProductDto> Value);
    }

    public class ProductDeleteService : IProductDeleteService
    {
        private readonly IDBContext DB;

        public ProductDeleteService(IDBContext dB)
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

            product.IsRemoved = true;
            DB.SaveChanges();
            result.Entity.Id = product.Id;
            result.Entity.Name = product.Name;
            result.Entity.Price = product.Price;
            result.Entity.Amount = product.Amount;

            return result;
        }
    }
}
