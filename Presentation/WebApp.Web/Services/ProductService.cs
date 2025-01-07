using Grpc.Core;
using WebApp.Grpc;

namespace WebApp.Web.Services
{
  public class ProductService : ProductServiceGRpc.ProductServiceGRpcClient
  {
    public override AsyncDuplexStreamingCall<ProductGRequestDto, ProductGDto> Create(CallOptions options)
    {
      return base.Create(options);
    }

    public override AsyncDuplexStreamingCall<ProductGRequestDto, ProductGDto> Create(Metadata headers = null, DateTime? deadline = null, CancellationToken cancellationToken = default)
    {
      return base.Create(headers, deadline, cancellationToken);
    }

  }
}
