using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Domain.Entity
{
  public class Product
  {
    public int Id { get; set; }

    [MaxLength(100)]
    public string? Name { get; set; }
    public float? Price { get; set; }
    public float? Amount { get; set; }

    public bool IsRemoved { get; set; }
  }

  
}
