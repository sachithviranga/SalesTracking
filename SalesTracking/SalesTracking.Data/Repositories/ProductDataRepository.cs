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

        public async Task<List<ProductDTO>> GetProducts()
        {

            var products = await _context.Product
                            .Include(i => i.ProductPrice)
                            .Where(a => a.IsActive == true)
                            .ToListAsync();


            var returnObj = _mapper.Map<List<ProductDTO>>(products);

            var result = await (from p in _context.Product
                                where p.IsActive == true
                                join pp in _context.ProductPrice on p.Id equals pp.ProductId
                                where pp.IsCurrent == true
                                select new
                                {
                                    Product = p,
                                    ProductPrice = pp
                                }).ToListAsync();

            var result2 = await _context.Product
                                  .Where(p => p.IsActive == true)
                                  .Join(_context.ProductPrice.Where(pp => pp.IsCurrent == true),
                                   p => p.Id,
                                   pp => pp.ProductId,
                                   (p, pp) => new { Product = p, ProductPrice = pp })
                                   .ToListAsync();


            return returnObj;

        }

        public async Task<int> AddProduct(ProductDTO product)
        {
            var saveObj = _mapper.Map<Product>(product);
            await _context.Product.AddAsync(saveObj);
            await _context.SaveChangesAsync();
            return saveObj.Id;

        }

        public async Task<ProductDTO> UpdateProduct(ProductDTO product)
        {
            var updateObj = await _context.Product.FirstOrDefaultAsync(a => a.Id == product.Id);
            if (updateObj != null)
            {
                updateObj.Name = product.Name;
                updateObj.IsActive = product.IsActive;
                updateObj.Updateby = product.UpdateBy;
                updateObj.UpdateDate = product.UpdateDate;

            }
            var endDate = DateTime.UtcNow;

            var getpriceObj = await _context.ProductPrice.Where(a => a.ProductId == product.Id && a.IsCurrent == true).ToListAsync();
            if (getpriceObj != null)
            {
                if (product.ProductPrice.Any())
                {
                    foreach (var updprices in getpriceObj)
                    {
                        var updPriceObj = await _context.ProductPrice.FirstOrDefaultAsync(a => a.ProductId == product.Id && a.IsCurrent == true);
                        updPriceObj.IsCurrent = false;
                        updPriceObj.EndDate = endDate;
                    }


                    var price = _mapper.Map<List<ProductPrice>>(product.ProductPrice);
                    await _context.ProductPrice.AddRangeAsync(price);
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
                    await _context.ProductPrice.AddRangeAsync(price);
                    foreach (var prices in price)
                    {
                        updateObj.ProductPrice.Add(prices);
                    }

                }

            }

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductDTO>(updateObj);
        }

        public async Task<ProductDTO> GetProductById(int id)
        {

            var product = await _context.Product.SingleOrDefaultAsync(a => a.Id == id);
            _context.Entry(product).Collection(s => s.ProductPrice).Query().Where(a => a.IsCurrent == true).Load();
            return _mapper.Map<ProductDTO>(product);

        }

        public async Task<ProductPriceDTO> GetSellingPriceByItem(int id)
        {

            var price = await _context.ProductPrice.SingleOrDefaultAsync(a => a.ProductId == id && a.IsCurrent == true);
            return _mapper.Map<ProductPriceDTO>(price);

        }


    }
}
