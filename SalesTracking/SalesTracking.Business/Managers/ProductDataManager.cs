using SalesTracking.Common.Common;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Data.Repositories;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Business.Managers
{
    public class ProductDataManager : IProductDataManager
    {
        private readonly IProductDataRepository _productDataRepository;

        private readonly IMapper<Object, ServiceResponse> _serviceResponseMapper;

        public ProductDataManager(IProductDataRepository productDataRepository, IMapper<object, ServiceResponse> serviceResponseMapper)
        {
            _productDataRepository = productDataRepository;
            _serviceResponseMapper = serviceResponseMapper;
        }

        public async Task<ServiceResponse> GetProducts()
        {
            var returnObj =await _productDataRepository.GetProducts();
            return _serviceResponseMapper.Map(returnObj);
        }

        

        public async Task<ServiceResponse> AddProduct(ProductDTO product)
        {
            product.CreateDate = DateTime.UtcNow;
            product.CreateBy = UserContext.Current;

            Parallel.ForEach(product.ProductPrice, a =>
            {
                a.IsActive = true;
                a.IsCurrent= true;
                a.CreateBy = UserContext.Current;
                a.CreateDate = DateTime.UtcNow;
                a.StartData = DateTime.UtcNow;
            });

            return _serviceResponseMapper.Map(_productDataRepository.AddProduct(product));

        }
        public async Task<ServiceResponse> UpdateProduct(ProductDTO product)
        {
            product.UpdateDate = DateTime.UtcNow;
            product.UpdateBy = UserContext.Current;

            Parallel.ForEach(product.ProductPrice, a =>
            {
                if (a.Id > 0)
                {
                    a.UpdateBy = UserContext.Current;
                    a.UpdateDate = DateTime.UtcNow;               
                    a.IsCurrent = true;
                    a.StartData= DateTime.UtcNow;

                }
                else 
                { 
                    a.IsActive = true;
                    a.IsCurrent = true;
                    a.CreateBy = UserContext.Current;
                    a.CreateDate = DateTime.UtcNow;
                    a.UpdateDate= DateTime.UtcNow;
                    a.UpdateBy= UserContext.Current;
                    a.StartData = DateTime.UtcNow;
                }
            });
            return _serviceResponseMapper.Map(_productDataRepository.UpdateProduct(product));
        }

        public async Task<ServiceResponse> GetProductById(int id)
        {
            var returnObj = _productDataRepository.GetProductById(id);
            return _serviceResponseMapper.Map(returnObj);
        }

        public async Task<ServiceResponse> GetSellingPriceByItem(int itemId)
        {
            var returnObj = _productDataRepository.GetSellingPriceByItem(itemId); 
            return _serviceResponseMapper.Map(returnObj);
        }



    }
}
