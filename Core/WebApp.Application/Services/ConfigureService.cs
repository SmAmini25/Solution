using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using WebApp.Application.Services.Create;
using WebApp.Application.Services.Delete;
using WebApp.Application.Services.Edit;
using WebApp.Application.Services.Facad;
using WebApp.Application.Services.Get;
using WebApp.Application.Services.GetList;

namespace WebApp.Application.Services
{
    public static class ConfigureService
    {
        public static void AddProductService(this IServiceCollection Service )
        {
            Service.AddScoped<IProductGetService, ProductGetService>();
            Service.AddScoped<IProductEditService, ProductEditService>();
            Service.AddScoped<IProductDeleteService, ProductDeleteService>();
            Service.AddScoped<IProductGetListService, ProductGetListService>();
            Service.AddScoped<IProductCreateService, ProductCreateService>();
            Service.AddScoped<IProductService, ProductService>();
        }
    }
}
