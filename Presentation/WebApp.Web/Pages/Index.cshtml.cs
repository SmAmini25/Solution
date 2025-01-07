using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Infrastructure.Comon;
using WebApp.Infrastructure.DTOs;

namespace WebApp.Web.Pages
{
  public class IndexModel : PageModel
  {
    private readonly ILogger<IndexModel> _logger;
    private readonly NLog.Logger Nlog = NLog.LogManager.GetCurrentClassLogger();

    private readonly IApiClient Api;

    public IndexModel(ILogger<IndexModel> logger, IApiClient api)
    {
      _logger = logger;
      Api = api;
    }

    public async void OnGet()
    {
      var result = await Api.GetAsync<ProductDto, ProductRequest>("api/Products", new());

      //_logger.Log(LogLevel.Information, "Test log MyGoldenDay");
      //_logger.Log( LogLevel.Information,  "Test log MyGoldenDay Trace");

      var p = result.Entity?.FirstOrDefault();


      _logger.LogInformation("Entity {result}", result);
      _logger.LogInformation("Product List {result}", result);

      Nlog.Debug(p);
      Nlog.Trace(result);
      Nlog.Trace(p);
      Nlog.Info(p);
      Nlog.Info(result);
      Nlog.Trace("Trace App ==>");
      Nlog.Debug("Debig ... MyGolden ");
      Nlog.Info("Info ....");
      Nlog.Warn("Warning ====> ");
      Nlog.Error($"Error Object for MyGoldenday ", "========================>", "Big bug Error");
      Nlog.Fatal($"Message for MyGoldenday ", "**********************************");
      //_logger.IsEnabled(LogLevel.Information);



      //_logger.Log<ResultList<ProductDto>>(LogLevel.Information, new EventId(169878), result, new Exception("Ok Error"));

    }
  }


}
