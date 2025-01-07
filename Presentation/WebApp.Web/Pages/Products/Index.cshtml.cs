using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Grpc.Net.Client;
using WebApp.Grpc;
using WebApp.Infrastructure.DTOs;
using Grpc.Core;


namespace WebApp.Web.Pages.Products
{
  public class IndexModel : PageModel
  {

    public List<ProductGDto> Product { get; set; }

    public async Task OnGetAsync()
    {
      GrpcChannel channel = GrpcChannel.ForAddress("https://localhost:7118/"); // در برنامه ها باید از appsettings خوانده شود
      ProductServiceGRpc.ProductServiceGRpcClient client = new(channel);

      Product = new();
      var list = client.GetList(new());
      await foreach (var item in list.ResponseStream.ReadAllAsync())
      {
        Product.Add(item);
      }
    }
  }
}
