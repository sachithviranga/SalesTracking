using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesTracking.Contracts.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.Product;
using SalesTracking.Entities.Sales;
using SalesTracking.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Data.Repositories
{
    public class StockBalanceRepository : IStockBalanceRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public StockBalanceRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        public int InsertStockBalance(List<StockBalanceDTO> StockBalances)
        {
            try
            {
                var saveObj = _mapper.Map<List<StockBalance>>(StockBalances);
                _context.StockBalance.AddRange(saveObj);
                return _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public bool CheckStockBalance(List<SalesDetailsDTO> CheckstockBalances, DateTime TransactionDate, out List<ProductDTO> products)
        {
            try
            {

                List<ProductDTO> insufProducts = _context.Product
                                                .Include(i => i.StockBalance)
                                                .AsEnumerable()
                                                .Where(a => CheckstockBalances.Any(x => x.ProductId == a.Id) &&
                                                 a.StockBalance.Where(s => s.TransactionDate <= TransactionDate).Sum(z => z.Qty) < CheckstockBalances.First(s => s.ProductId == a.Id).Qty)
                                                .Select(s => new ProductDTO { Id = s.Id, Name = s.Name })
                                                .ToList();





                //List<ProductDTO> insufProducts = _context.StockBalance
                //            .Include(i => i.Product)
                //            .AsEnumerable()
                //            .Where(s => CheckstockBalances.Any(x => x.ProductId == s.ProductId) && s.TransactionDate <= TransactionDate)
                //            .GroupBy(x => new { x.ProductId, x.Product.Name })
                //            .Select(x => new { ProductId = x.Key.ProductId, ProductName = x.Key.Name, BalanceQty = x.Sum(s => s.Qty) })
                //            .Where(s => CheckstockBalances.Any(w => w.ProductId == s.ProductId && w.Qty > s.BalanceQty))
                //            .Select(s => new ProductDTO { Id = s.ProductId, Name = s.ProductName })
                //            .ToList();



                products = insufProducts.ToList();

                return !insufProducts.Any();

            }
            catch
            {
                throw;
            }
        }

        public int UpdateStockBalance(List<StockBalanceDTO> stockbalances, DateTime TransactionDate)
        {
            try
            {
                List<StockBalanceDTO> stockBalanceUpd = new();

                foreach (var stockbal in stockbalances)
                {
                    int prodId = stockbal.ProductId;
                    decimal prodQty = stockbal.Qty;
                    decimal balQty = prodQty;
                    decimal updQty = 0;

                    var getStockObj = _context.StockBalance
                        .Where(a => a.ProductId == prodId && a.TransactionDate <= TransactionDate)
                        .GroupBy(a => new { a.ProductId, a.BatchId })
                        .Select(a => new
                        {
                            a.Key.ProductId,
                            a.Key.BatchId,
                            Qty = a.Sum(s => s.Qty),
                            TransactionDate = a.FirstOrDefault(s => s.Qty > 0).TransactionDate
                        })
                        .Where(w => w.Qty > 0)
                        .OrderBy(o => o.TransactionDate)
                        .ToList();

                    if (getStockObj != null && getStockObj.Any())
                    {
                        foreach (var updBal in getStockObj)
                        {

                            if (balQty > 0)
                            {
                                int stkQty = updBal.Qty;
                                var batchId = updBal.BatchId;

                                if (stkQty > balQty)
                                {
                                    updQty = balQty;
                                    balQty = 0;
                                }
                                else
                                {
                                    balQty = balQty - stkQty;
                                    updQty = stkQty;
                                }
                                stockBalanceUpd.Add(new StockBalanceDTO
                                {
                                    CreateBy = stockbal.CreateBy,
                                    CreateDate = DateTime.UtcNow,
                                    IsActive = stockbal.IsActive,
                                    ProductId = stockbal.ProductId,
                                    ReferenceId = stockbal.ReferenceId,
                                    ReferenceLineId = stockbal.ReferenceLineId,
                                    SellPrice = stockbal.SellPrice,
                                    UnitPrice = stockbal.UnitPrice,
                                    TransactionDate = TransactionDate,
                                    BatchId = batchId,
                                    Qty = updQty * (-1),
                                    ReferenceType = stockbal.ReferenceType,
                                });

                                if (balQty == 0) break;
                            }

                        }
                    }
                }
                var saveObj = _mapper.Map<List<StockBalance>>(stockBalanceUpd);
                _context.StockBalance.AddRange(saveObj);
                return _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }



        public List<ProductQtyDTO> GetAvaibleProductQty(bool isAllProduct = true)
        {
            try
            {
                var balance = _context.StockBalance
                      .Include(i => i.Product)
                      .GroupBy(a => new { a.ProductId, a.Product.Name })
                      .Select(s => new ProductQtyDTO
                      {
                          ProductId = s.Key.ProductId,
                          ProductName = s.Key.Name,
                          Qty = s.Sum(a => a.Qty),
                      }).ToList();

                if (isAllProduct)
                {
                    if (!balance.Any())
                    {
                        balance.AddRange(
                            _context.Product
                            .Where(a => a.IsActive == true)
                            .Select(s => new ProductQtyDTO
                            {
                                ProductId = s.Id,
                                ProductName = s.Name,
                                Qty = 0,
                            }).ToList());
                    }
                    else
                    {

                        balance.AddRange(_context.Product
                                    .AsEnumerable()
                                    .Where(a => a.IsActive == true && !balance.Any(s => s.ProductId == a.Id))
                                    .Select(s => new ProductQtyDTO
                                    {
                                        ProductId = s.Id,
                                        ProductName = s.Name,
                                        Qty = 0,
                                    }).ToList());
                    }
                }

                return balance;
            }
            catch (Exception)
            {
                throw;
            }
        }



    }
}
