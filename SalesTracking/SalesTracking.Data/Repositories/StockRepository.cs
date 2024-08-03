using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesTracking.Contracts.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.Customer;
using SalesTracking.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Data.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public StockRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<StockPurchaseDTO> GetStock()
        {
            try
            {
                var stock = _context.StockPurchase
                            .Where(a => a.IsActive == true)
                            .Include(i => i.StockPurchaseDetails).Include(a => a.StockPurchasePayment)
                            .OrderByDescending(i => i.Id)
                            .ToList();

                return _mapper.Map<List<StockPurchaseDTO>>(stock);
            }
            catch
            {
                throw;
            }


        }

        public int AddStock(StockPurchaseDTO stock)
        {
            try
            {
                var saveObj = _mapper.Map<StockPurchase>(stock);
                _context.StockPurchase.Add(saveObj);
                _context.SaveChanges();
                return saveObj.Id;
            }
            catch
            {
                throw;

            }

        }

        public StockPurchaseDTO UpdateStock(StockPurchaseDTO stock)
        {
            try
            {
                var updateObj = _context.StockPurchase.FirstOrDefault(a => a.Id == stock.Id);
                if (updateObj != null)
                {
                    _context.Entry(updateObj).Collection(l => l.StockPurchaseDetails).Load();


                    if (updateObj.StockPurchaseDetails.Any())
                    {
                        _context.RemoveRange(updateObj.StockPurchaseDetails);
                    }

                    _context.Entry(updateObj).Collection(l => l.StockPurchasePayment).Load();

                    if (updateObj.StockPurchasePayment.Any())
                    {
                        _context.RemoveRange(updateObj.StockPurchasePayment);

                    }

                    updateObj.PurchaseNo = stock.PurchaseNo;
                    updateObj.TransactionDate =  stock.TransactionDate.LocalDateTime.Date;
                    updateObj.IsActive = stock.IsActive;
                    updateObj.UpdateBy = stock.UpdateBy;
                    updateObj.UpdateDate = stock.UpdateDate;

                    if (stock.StockPurchaseDetails.Any())
                    {
                        var stockdetails = _mapper.Map<List<StockPurchaseDetails>>(stock.StockPurchaseDetails);
                        _context.StockPurchaseDetails.AddRange(stockdetails);//455555
                        foreach (var stockdetail in stockdetails)
                        {
                            updateObj.StockPurchaseDetails.Add(stockdetail);
                        }
                    }

                    if (stock.StockPurchasePayment.Any())
                    {
                        var stockpayments = _mapper.Map<List<StockPurchasePayment>>(stock.StockPurchasePayment);
                        _context.StockPurchasePayment.AddRange(stockpayments);//455555
                        foreach (var stockpayment in stockpayments)
                        {
                            updateObj.StockPurchasePayment.Add(stockpayment);
                        }
                    }

                    _context.StockPurchase.Update(updateObj);
                    _context.SaveChanges();
                }
                return _mapper.Map<StockPurchaseDTO>(updateObj);
            }
            catch
            {
                throw;

            }
        }

        public List<StockPurchaseDTO> GetStockPayment()
        {
            try
            {
                var stockpayment = _context.StockPurchase
                            .Where(a => a.IsActive == true)
                            .Include(i => i.StockPurchasePayment)
                            .ToList();

                return _mapper.Map<List<StockPurchaseDTO>>(stockpayment);
            }
            catch
            {
                throw;
            }


        }

        public StockPurchaseDTO GetStockById(int id)
        {
            try
            {
                var stockid = _context.StockPurchase.Where(a => a.Id == id)
                    .Include(i => i.StockPurchaseDetails).ThenInclude(i => i.Product)
                    .Include(a => a.StockPurchasePayment).ThenInclude(a => a.PaymentType)
                    .SingleOrDefault();

                return _mapper.Map<StockPurchaseDTO>(stockid);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public StockPurchaseDTO ApproveStock(StockPurchaseDTO stock)
        {
            try
            {
                var updateObj = _context.StockPurchase.FirstOrDefault(a => a.Id == stock.Id);
                if (updateObj != null)
                {
                    _context.Entry(updateObj).Collection(l => l.StockPurchaseDetails).Load();


                    if (updateObj.StockPurchaseDetails.Any())
                    {
                        _context.RemoveRange(updateObj.StockPurchaseDetails);
                    }

                    _context.Entry(updateObj).Collection(l => l.StockPurchasePayment).Load();

                    if (updateObj.StockPurchasePayment.Any())
                    {
                        _context.RemoveRange(updateObj.StockPurchasePayment);

                    }

                    updateObj.PurchaseNo = stock.PurchaseNo;
                    updateObj.TransactionDate = stock.TransactionDate.LocalDateTime.Date;
                    updateObj.IsActive = stock.IsActive;
                    updateObj.UpdateBy = stock.UpdateBy;
                    updateObj.UpdateDate = stock.UpdateDate;
                    updateObj.ApprovedDate = stock.ApprovedDate;
                    updateObj.ApprovedBy = stock.ApprovedBy;
                    updateObj.IsApproved = stock.IsApproved;

                    if (stock.StockPurchaseDetails.Any())
                    {
                        var stockdetails = _mapper.Map<List<StockPurchaseDetails>>(stock.StockPurchaseDetails);
                        _context.StockPurchaseDetails.AddRange(stockdetails);//455555
                        foreach (var stockdetail in stockdetails)
                        {
                            updateObj.StockPurchaseDetails.Add(stockdetail);
                        }
                    }

                    if (stock.StockPurchasePayment.Any())
                    {
                        var stockpayments = _mapper.Map<List<StockPurchasePayment>>(stock.StockPurchasePayment);
                        _context.StockPurchasePayment.AddRange(stockpayments);//455555
                        foreach (var stockpayment in stockpayments)
                        {
                            updateObj.StockPurchasePayment.Add(stockpayment);
                        }
                    }

                    _context.StockPurchase.Update(updateObj);
                    _context.SaveChanges();
                    return _mapper.Map<StockPurchaseDTO>(updateObj);
                }
                // this.AddStock(stock);
                else
                {
                    var saveObj = _mapper.Map<StockPurchase>(stock);
                    _context.StockPurchase.Add(saveObj);
                    _context.SaveChanges();
                    return _mapper.Map<StockPurchaseDTO>(saveObj);

                }

               
            }
            catch
            {
                throw;

            }
        }

        public List<StockBalanceDTO> GetStockBySellprice(int id)
        {
            try
            {
                var stock = _context.StockBalance.Where(a => a.ProductId == id).ToList();
                return _mapper.Map<List<StockBalanceDTO>>(stock);
            }
            catch
            {
                throw;
            }


        }
    }
}
