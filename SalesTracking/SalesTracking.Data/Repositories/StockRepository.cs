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

        public async Task<List<StockPurchaseDTO>> GetStock()
        {
            var stock = await _context.StockPurchase
                        .Where(a => a.IsActive == true)
                        .Include(i => i.StockPurchaseDetails).Include(a => a.StockPurchasePayment)
                        .OrderByDescending(i => i.Id)
                        .ToListAsync();

            return _mapper.Map<List<StockPurchaseDTO>>(stock);
        }

        public async Task<int> AddStock(StockPurchaseDTO stock)
        {

            var saveObj = _mapper.Map<StockPurchase>(stock);
            await _context.StockPurchase.AddAsync(saveObj);
            await _context.SaveChangesAsync();
            return saveObj.Id;

        }

        public async Task<StockPurchaseDTO> UpdateStock(StockPurchaseDTO stock)
        {
                var updateObj =await _context.StockPurchase.FirstOrDefaultAsync(a => a.Id == stock.Id);
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

                    if (stock.StockPurchaseDetails.Any())
                    {
                        var stockdetails = _mapper.Map<List<StockPurchaseDetails>>(stock.StockPurchaseDetails);
                        await _context.StockPurchaseDetails.AddRangeAsync(stockdetails);//455555
                        foreach (var stockdetail in stockdetails)
                        {
                            updateObj.StockPurchaseDetails.Add(stockdetail);
                        }
                    }

                    if (stock.StockPurchasePayment.Any())
                    {
                        var stockpayments = _mapper.Map<List<StockPurchasePayment>>(stock.StockPurchasePayment);
                        await _context.StockPurchasePayment.AddRangeAsync(stockpayments);//455555
                        foreach (var stockpayment in stockpayments)
                        {
                            updateObj.StockPurchasePayment.Add(stockpayment);
                        }
                    }

                    _context.StockPurchase.Update(updateObj);
                     await _context.SaveChangesAsync();
                }
                return _mapper.Map<StockPurchaseDTO>(updateObj);
        }

        public async Task<List<StockPurchaseDTO>> GetStockPayment()
        {

                var stockpayment = await _context.StockPurchase
                            .Where(a => a.IsActive == true)
                            .Include(i => i.StockPurchasePayment)
                            .ToListAsync();

                return _mapper.Map<List<StockPurchaseDTO>>(stockpayment);

        }

        public async Task<StockPurchaseDTO> GetStockById(int id)
        {

                var stock =await _context.StockPurchase.Where(a => a.Id == id)
                    .Include(i => i.StockPurchaseDetails).ThenInclude(i => i.Product)
                    .Include(a => a.StockPurchasePayment).ThenInclude(a => a.PaymentType)
                    .SingleOrDefaultAsync();

                return _mapper.Map<StockPurchaseDTO>(stock);

        }

        public async Task<StockPurchaseDTO> ApproveStock(StockPurchaseDTO stock)
        {
            var updateObj = await _context.StockPurchase.FirstOrDefaultAsync(a => a.Id == stock.Id);
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
                await _context.SaveChangesAsync();
                return _mapper.Map<StockPurchaseDTO>(updateObj);
            }
            else
            {
                var saveObj = _mapper.Map<StockPurchase>(stock);
                await _context.StockPurchase.AddAsync(saveObj);
                await _context.SaveChangesAsync();
                return _mapper.Map<StockPurchaseDTO>(saveObj);

            }
        }

        public async Task<List<StockBalanceDTO>> GetStockBySellprice(int id)
        {
            var stock = await _context.StockBalance.Where(a => a.ProductId == id).ToListAsync();
            return _mapper.Map<List<StockBalanceDTO>>(stock);
        }
    }
}
