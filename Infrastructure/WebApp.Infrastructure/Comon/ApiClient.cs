using Microsoft.Extensions.Configuration;
using RestSharp;
using System.Net;

namespace WebApp.Infrastructure.Comon
{
  public interface IApiClient
  {
    //private string? GetTokenFromCookies();



    // متد GET
    Task<ResultList<T>> GetAsync<T, K>(string endpoint, Request<K> Value)
      where T : class, new()
      where K : class, new();

  }

  public class ApiClient : IApiClient
  {
    private readonly IConfiguration Configuration;
    private readonly string BaseUrl;
    //private readonly RestClient _client;


    public ApiClient(IConfiguration configuration)
    {
      Configuration = configuration;
      BaseUrl = configuration["BaseUrl"] ?? "https://localhost:7219/";
      //_client = new RestClient(baseUrl);
    }




    // متد GET
    public async Task<ResultList<T>> GetAsync<T, K>(string Resource, Request<K> Value)
      where T : class, new()
      where K : class, new()
    {
      ResultList<T> result = new ResultList<T>();

      var request = new RestRequest(Resource, Method.Get);
      // SetAuthorizationHeader(request);
      //request.AddQueryParameter<Request<K>>("value", Value);
      RestClient client = new RestClient(BaseUrl);

      var response = client.Execute<ResultList<T>>(request);
      if (response.StatusCode == HttpStatusCode.OK && response.IsSuccessful == true)
      {
        return response.Data;
      }


      return result;

    }


  }
}
