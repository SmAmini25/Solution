using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Infrastructure.DTOs
{
  public class ProductDto
  {
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    public float? Price { get; set; }
    public float? Amount { get; set; }

    public override string ToString()
    {
      return $"{Id} {Name}";
    }

  }

  public class ProductRequest
  {
    public int Id { get; set; }
    public string? Name { get; set; }
  }
}
