using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApp.Application.Services.Create;
using WebApp.Application.Services.Delete;
using WebApp.Application.Services.Edit;
using WebApp.Application.Services.Get;
using WebApp.Application.Services.GetList;

namespace WebApp.Application.Services.Facad
{
    public interface IProductService
    {
        IProductCreateService Create { get; }
        IProductEditService Edit { get; }
        IProductGetService Get { get; }
        IProductGetListService GetList { get; }
        IProductDeleteService Delete { get; }
    }

    public class ProductService : IProductService
    {
        public ProductService(IProductCreateService create, IProductEditService edit, IProductGetService get, IProductGetListService getList, IProductDeleteService delete)
        {
            Create = create;
            Edit = edit;
            Get = get;
            GetList = getList;
            Delete = delete;
        }

        public IProductCreateService Create { get; }
        public IProductEditService Edit { get; }
        public IProductGetService Get { get; }
        public IProductGetListService GetList { get; }
        public IProductDeleteService Delete { get; }
    }
}
