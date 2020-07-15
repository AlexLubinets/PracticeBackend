using AutoMapper;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using ProductApp.BLL.Constants;
using ProductApp.BLL.Interfaces;
using ProductApp.BLL.Models;
using ProductApp.DAL.Entities;
using ProductApp.DAL.Interfaces;

namespace ProductApp.BLL.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper mapper;
        private readonly IUnitOfWork uow;
        private readonly IPagingService<ProductDTO> pagingService;

        public ProductService(IMapper mapper, IUnitOfWork uow, IPagingService<ProductDTO> pagingService)
        {
            this.mapper = mapper;
            this.uow = uow;
            this.pagingService = pagingService;
        }

        public PagedList<ProductDTO> GetProductsSegment(ProductPagingParams pagingParams)
        {
            IQueryable<Product> products = uow.ProductRepository.GetAllItems();

            products = SortProducts(products, pagingParams);

            IEnumerable<ProductDTO> productsDTO = mapper.Map<IEnumerable<ProductDTO>>(products.AsEnumerable());

            return pagingService.ToPagedList(productsDTO,
                pagingParams.PageNumber, pagingParams.PageSize);
        }

        public IQueryable<Product> SortProducts(IQueryable<Product> products, ProductPagingParams pagingParams)
        {
            switch (pagingParams.SortOrder)
            {
                case SortProductState.NameDesc:
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case SortProductState.PriceAsc:
                    products = products.OrderBy(s => s.Price);
                    break;
                case SortProductState.PriceDesc:
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Name);
                    break;
            }
            return products;
        }

        public async Task<ProductDTO> GetProductById(int id)
        {
            var item = await uow.ProductRepository.GetItem(id);
            return mapper.Map<ProductDTO>(item);
        }

        public async Task<ProductDTO> AddProduct(ProductDTO model)
        {
            model = ValidateModel(model);
            await uow.ProductRepository.Create(mapper.Map<Product>(model));
            await uow.Save();
            return model;
        }

        public async Task<ProductDTO> ChangeProduct(ProductDTO modelChanges)
        {
            modelChanges = ValidateModel(modelChanges);
            uow.ProductRepository.Update(mapper.Map<Product>(modelChanges));
            await uow.Save();
            return modelChanges;
        }

        public async Task DeleteProductById(int id)
        {
            await uow.ProductRepository.Delete(id);
            await uow.Save();
        }

        public ProductDTO ValidateModel(ProductDTO model)
        {
            if (model.Name.Length >= 50 || model.Name.Length == 0)
                throw new ArgumentException();
            model.Price = Math.Round(model.Price, 2, MidpointRounding.AwayFromZero);
            if (model.Price >= 10000 || model.Price <= 0)
                throw new ArgumentOutOfRangeException();
            return model;
        }
    }
}
