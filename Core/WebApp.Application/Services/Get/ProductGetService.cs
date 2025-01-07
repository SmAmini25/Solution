using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Entity;
using WebApp.Domain.Interface;
using WebApp.Infrastructure.Comon;
using WebApp.Infrastructure.DTOs;

namespace WebApp.Application.Services.Get
{
  public interface IProductGetService
    {
        Result<ProductDto> Execute(Request<ProductRequest> Value);
    }


    public class ProductGetService : IProductGetService
    {
        private readonly IDBContext DB;

        public ProductGetService(IDBContext dB)
        {
            DB = dB;
        }

        public Result<ProductDto> Execute(Request<ProductRequest> Value)
        {
            Result<ProductDto> result = new Result<ProductDto>();

            if (Value.Id is null)
            {
                result.State = State.NotFound;
                return result;
            }

            Product? product = DB.Product.Where(x=>x.IsRemoved==false).FirstOrDefault(x => x.Id == Value.Id);

            if (product == null)
            {
                result.State = State.NotFound;
                return result;
            }

            result.Entity.Id = product.Id;
            result.Entity.Name = product.Name;
            result.Entity.Price= product.Price;
            result.Entity.Amount = product.Amount;

            return result;
        }
    }
}
