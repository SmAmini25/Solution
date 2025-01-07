using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Entity;
using WebApp.Domain.Interface;
using WebApp.Infrastructure.Comon;
using WebApp.Infrastructure.DTOs;

namespace WebApp.Application.Services.Create
{
  public interface IProductCreateService
    {
        Result<ProductDto> Execute(Request<ProductRequest, ProductDto> Value);
    }

    public class ProductCreateService : IProductCreateService
    {
        private readonly IDBContext DB;

        public ProductCreateService(IDBContext dB)
        {
            DB = dB;
        }

        public Result<ProductDto> Execute(Request<ProductRequest, ProductDto> Value)
        {
            Result<ProductDto> result = new();

            Product product = new();
            product.Name = Value.Entity.Name;
            product.Price = Value.Entity.Price;
            product.Amount = Value.Entity.Amount;
            DB.Product.Add(product);
            DB.SaveChanges();

            result.Entity.Id = product.Id;
            result.Entity.Name = product.Name;
            result.Entity.Price = product.Price;
            result.Entity.Amount = product.Amount;

            return result;
        }
    }
}

