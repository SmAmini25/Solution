using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Domain.Entity;
using WebApp.Domain.Interface;
using WebApp.Infrastructure.Comon;
using System.Linq;
using WebApp.Infrastructure.DTOs;

namespace WebApp.Application.Services.GetList
{
  public interface IProductGetListService
    {
        ResultList<ProductDto> Execute(Request<ProductRequest> Value);
    }

    public class ProductGetListService : IProductGetListService
    {
        private readonly IDBContext DB;

        public ProductGetListService(IDBContext dB)
        {
            DB = dB;
        }

        public ResultList<ProductDto> Execute(Request<ProductRequest> Value)
        {
            ResultList<ProductDto> result = new();

            var qry = DB.Product.Where(x => x.IsRemoved == false).AsQueryable();
            if (!string.IsNullOrWhiteSpace(Value.Date.Name))
            {
                qry = qry.Where(x => x.Name.Contains(Value.Date.Name));
            }

            var listResult = qry.ToList();
            foreach (var item in listResult)
            {
                ProductDto product = new ProductDto();
                product.Id = item.Id;
                product.Name = item.Name;
                product.Amount = item.Amount;
                product.Price = item.Price;
                result.Entity.Add(product);
            }

            return result;
        }
    }
}
