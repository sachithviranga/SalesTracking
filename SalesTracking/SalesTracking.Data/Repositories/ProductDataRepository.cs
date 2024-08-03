using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesTracking.Contracts.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.Customer;
using SalesTracking.Entities.Product;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Data.Repositories
{
    public class ProductDataRepository : IProductDataRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public ProductDataRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<ProductDTO> GetProducts()
        {
            try
            {
                //var products = _context.Product.Where(a => a.IsActive == true).ToList();

               // var product = _context.Product.Where(a => a.IsActive == true).ToList();
                //var productprice = _context.ProductPrice.Where(a => a.IsCurrent == true).ToList();


                var products = _context.Product
                    .Include(i=>i.ProductPrice)
                    .Where(a => a.IsActive == true)
                    .ToList();


                var returnObj = _mapper.Map<List<ProductDTO>>(products);

                var result = (from p in _context.Product where p.IsActive == true
                              join pp in _context.ProductPrice on p.Id equals pp.ProductId where pp.IsCurrent==true                             
                              select new
                              
                              {
                                  Product = p,
                                  ProductPrice = pp
                              }).ToList();

                var result2 = _context.Product
                                      .Where(p => p.IsActive==true)
                                      .Join(_context.ProductPrice.Where(pp => pp.IsCurrent==true),
                                       p => p.Id,
                                       pp => pp.ProductId,
                                       (p, pp) => new { Product = p, ProductPrice = pp })
                                       .ToList();


                return returnObj;

            }
            catch
            {
                throw;

            }
        }

        public int AddProduct(ProductDTO product)
        {
            try
            {
                var saveObj = _mapper.Map<Product>(product);
                _context.Product.Add(saveObj);
                _context.SaveChanges();
                return saveObj.Id;
            }
            catch
            {
                throw;

            }

        }

        public ProductDTO UpdateProduct(ProductDTO product)
        {
            try
            {
                var updateObj = _context.Product.FirstOrDefault(a => a.Id == product.Id);
                if (updateObj != null)
                {
                    updateObj.Name = product.Name;              
                    updateObj.IsActive = product.IsActive;
                    updateObj.Updateby = product.UpdateBy;
                    updateObj.UpdateDate = product.UpdateDate;
                   
                }
                var endDate = DateTime.UtcNow;

                //var getpriceObj = _context.ProductPrice.FirstOrDefault(a => a.ProductId == product.Id);

                 var getpriceObj = _context.ProductPrice.Where(a => a.ProductId == product.Id && a.IsCurrent==true).ToList();
                if (getpriceObj != null)
                {
                    if (product.ProductPrice.Any())
                    {
                        foreach (var updprices in getpriceObj)  
                        {
                            var updPriceObj = _context.ProductPrice.FirstOrDefault(a => a.ProductId == product.Id && a.IsCurrent == true);
                            updPriceObj.IsCurrent = false;
                            updPriceObj.EndDate = endDate;
                        }
                                       

                        var price = _mapper.Map<List<ProductPrice>>(product.ProductPrice);
                        _context.ProductPrice.AddRange(price);//455555
                        foreach (var prices in price)
                        {
                            updateObj.ProductPrice.Add(prices);
                        }

                    }
                   

                }
                else
                {
                    if (product.ProductPrice.Any())
                    {
                       
                        var price = _mapper.Map<List<ProductPrice>>(product.ProductPrice);
                        _context.ProductPrice.AddRange(price);//455555
                        foreach (var prices in price)
                        {
                            updateObj.ProductPrice.Add(prices);
                        }

                    }

                }

                _context.SaveChanges();
                return _mapper.Map<ProductDTO>(updateObj);
            }
            catch
            {
                throw;

            }
        }

        public ProductDTO GetProductById(int id)
        {
            try
            {
                var product = _context.Product.SingleOrDefault(a => a.Id == id);
                _context.Entry(product).Collection(s => s.ProductPrice).Query().Where(a => a.IsCurrent == true).Load();
                return _mapper.Map<ProductDTO>(product);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ProductPriceDTO GetSellingPriceByItem(int id) 
        {
            try
            {
                var price = _context.ProductPrice.SingleOrDefault(a => a.ProductId == id && a.IsCurrent == true);
                return _mapper.Map<ProductPriceDTO>(price);
            }
            catch(Exception)
            {
                throw;
            }
        }


    }
}
