using Grpc.Net.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Grpc;
using WebApp.Infrastructure.DTOs;

namespace WebApp.Web.Pages.Products
{
  public class EditModel : PageModel
  {
    [BindProperty]
    public ProductDto? Product { get; set; }


    public async Task OnGetAsync(int? id)
    {
      GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7118/"); // در برنامه ها باید از appsettings خوانده شود
      ProductServiceGRpc.ProductServiceGRpcClient client = new(channel);

      if (id.HasValue)
      {
        var product = client.Get(new() { Id = id.Value });
        if (product.State == State.Ok)
        {
          Product = new();
          Product.Id = product.Id;
          Product.Name = product.Name;
          Product.Price = product.Price;
          Product.Amount = product.Amount;
        }
        else
        {
          ModelState.AddModelError("", product.ErrorMessage);
        }
      }
      else
      {
        ModelState.AddModelError("", "Not Found Object");
      }
    }


    public async Task<IActionResult> OnPostAsync(int? id)
    {
      GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7118/"); // در برنامه ها باید از appsettings خوانده شود
      ProductServiceGRpc.ProductServiceGRpcClient client = new(channel);

      if (ModelState.IsValid)
      {
        if (id.HasValue)
        {
          ProductGRequestDto ResultProduct = new();
          ResultProduct.Id = id.Value;
          ResultProduct.Name = this.Product.Name;
          ResultProduct.Price = this.Product.Price ?? 0;
          ResultProduct.Amount = this.Product.Amount ?? 0;
          var Product = await client.UpDateAsync(ResultProduct);
          if (Product.State == State.Ok)
          {
            return Redirect("/Product");
          }
          else
          {
            ModelState.AddModelError("", Product.ErrorMessage);
          }
        }
        else
        {
          ModelState.AddModelError("", "محصول یافت نشد.");
        }
      }

      return Page();
    }
  }
}
