using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Infrastructure.Comon
{
  public class Result<T>
     where T : class, new()
  {
    public Result()
    {
      Entity = new();
    }

    public T? Entity { get; set; }

    public string ErrorMessage { get; set; }

    public State State { get; set; } = State.OK;
  }

  public class ResultList<T>
     where T : class, new()
  {
    public ResultList()
    {
      Entity = new();
    }

    public List<T>? Entity { get; set; }

    public State State { get; set; } = State.OK;

    public override string ToString()
    {
      return $"{State} {DateTime.Now.TimeOfDay}";
    }
  }

  public class Request<T>
      where T : class, new()
  {
    public long? Id { get; set; }

    public T? Date { get; set; } = new();

    public State State { get; set; } = State.OK;

    public override string ToString()
    {
      return $"{State} {DateTime.Now.TimeOfDay}";
    }
  }

  public class Request<T, K>
      where T : class, new()
      where K : class, new()
  {
    public Request()
    {
      Entity = new();
      Data = new();
    }

    // public long? ID { get; set; }

    public T? Data { get; set; }
    public K? Entity { get; set; }
  }

  public enum State
  {
    OK = 0,
    NotFound = 1,
    AccessDenied = 2,
    Error = 3
  }
}
