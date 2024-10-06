using Microsoft.EntityFrameworkCore;
using SalesTracking.Common.Common;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Data.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static SalesTracking.Common.Constant;

namespace SalesTracking.Business.Managers
{
    public class StockManager : IStockManager
    {
        private readonly IStockRepository _stockRepository;

        private readonly IStockBalanceRepository _stockBalanceRepository;

        private readonly IMapper<Object, ServiceResponse> _serviceResponseMapper;

        public StockManager(IStockRepository stockRepository, IStockBalanceRepository stockBalanceRepository, IMapper<object, ServiceResponse> serviceResponseMapper)
        {
            _stockRepository = stockRepository;
            _stockBalanceRepository = stockBalanceRepository;
            _serviceResponseMapper = serviceResponseMapper;
        }

        public async Task<ServiceResponse> GetStock()
        {
            var returnObj = await _stockRepository.GetStock();
            return _serviceResponseMapper.Map(returnObj);
        }

        public async Task<ServiceResponse> AddStock(StockPurchaseDTO stock)
        {
            stock.CreateDate = DateTime.UtcNow;
            stock.CreateBy = UserContext.Current;

            Parallel.ForEach(stock.StockPurchaseDetails, a =>
            {
                a.CreateBy = UserContext.Current;
                a.CreateDate = DateTime.UtcNow;

                if (a.Id > 0)
                {
                    a.UpdateBy = UserContext.Current;
                    a.UpdateDate = DateTime.UtcNow;
                }
                else
                {
                    a.IsActive = true;
                    a.CreateBy = UserContext.Current;
                    a.CreateDate = DateTime.UtcNow;
                }
            });

            // note : save payments
            Parallel.ForEach(stock.StockPurchasePayment, a =>
            {
                a.CreateBy = UserContext.Current;
                a.CreateDate = DateTime.UtcNow;

                if (a.Id > 0)
                {
                    a.UpdateBy = UserContext.Current;
                    a.UpdateDate = DateTime.UtcNow;
                }
                else
                {
                    a.IsActive = true;
                    a.CreateBy = UserContext.Current;
                    a.CreateDate = DateTime.UtcNow;
                }
            });

            return _serviceResponseMapper.Map(await _stockRepository.AddStock(stock));

        }

        public async Task<ServiceResponse> UpdateStock(StockPurchaseDTO stock)
        {
            stock.UpdateDate = DateTime.UtcNow;
            stock.UpdateBy = UserContext.Current;

            Parallel.ForEach(stock.StockPurchaseDetails, a =>
            {
                if (a.Id > 0)
                {
                    a.UpdateBy = UserContext.Current;
                    a.UpdateDate = DateTime.UtcNow;
                }
                else
                {
                    a.IsActive = true;
                    a.CreateBy = UserContext.Current;
                    a.CreateDate = DateTime.UtcNow;
                }
            });

            // note : save payments
            Parallel.ForEach(stock.StockPurchasePayment, a =>
            {
                a.CreateBy = UserContext.Current;
                a.CreateDate = DateTime.UtcNow;

                if (a.Id > 0)
                {
                    a.UpdateBy = UserContext.Current;
                    a.UpdateDate = DateTime.UtcNow;
                }
                else
                {
                    a.IsActive = true;
                    a.CreateBy = UserContext.Current;
                    a.CreateDate = DateTime.UtcNow;
                }
            });

            return _serviceResponseMapper.Map(await _stockRepository.UpdateStock(stock));
        }

        public async Task<ServiceResponse> GetStockPayment()
        {
            var returnObj = await _stockRepository.GetStockPayment();
            return _serviceResponseMapper.Map(returnObj);
        }

        public async Task<ServiceResponse> GetStockById(int id)
        {
            var returnObj = await _stockRepository.GetStockById(id);
            return _serviceResponseMapper.Map(returnObj);
        }

        public async Task<ServiceResponse> ApproveStock(StockPurchaseDTO stock)
        {
            using var scope = new TransactionScope();
            if (stock.Id > 0)
            {
                stock.UpdateDate = DateTime.UtcNow;
                stock.UpdateBy = UserContext.Current;
                stock.ApprovedDate = DateTime.UtcNow;
                stock.ApprovedBy = UserContext.Current;
                stock.IsApproved = true;

            }
            else
            {
                stock.UpdateDate = DateTime.UtcNow;
                stock.UpdateBy = UserContext.Current;
                stock.ApprovedDate = DateTime.UtcNow;
                stock.ApprovedBy = UserContext.Current;
                stock.IsApproved = true;
                stock.CreateBy = UserContext.Current;
                stock.CreateDate = DateTime.UtcNow;
            }


            Parallel.ForEach(stock.StockPurchaseDetails, a =>
            {
                if (a.Id > 0)
                {
                    a.UpdateBy = UserContext.Current;
                    a.UpdateDate = DateTime.UtcNow;
                }
                else
                {
                    a.IsActive = true;
                    a.CreateBy = UserContext.Current;
                    a.CreateDate = DateTime.UtcNow;
                }
            });

            // note : save payments
            Parallel.ForEach(stock.StockPurchasePayment, a =>
            {
                if (a.Id > 0)
                {
                    a.UpdateBy = UserContext.Current;
                    a.UpdateDate = DateTime.UtcNow;
                    a.ApprovedBy = UserContext.Current;
                    a.ApprovedDate = DateTime.UtcNow;
                    a.IsApproved = true;
                }
                else
                {
                    a.IsActive = true;
                    a.CreateBy = UserContext.Current;
                    a.CreateDate = DateTime.UtcNow;
                    a.ApprovedBy = UserContext.Current;
                    a.ApprovedDate = DateTime.UtcNow;
                    a.IsApproved = true;
                }
            });

            var savedObj = await _stockRepository.ApproveStock(stock);

            List<StockBalanceDTO> stockBalances = new();

            Parallel.ForEach(savedObj.StockPurchaseDetails, a =>
            {
                stockBalances.Add(new StockBalanceDTO
                {
                    CreateBy = UserContext.Current,
                    CreateDate = DateTime.UtcNow,
                    IsActive = a.IsActive,
                    ProductId = a.ProductId,
                    BatchId = GenerateBatch.GenerateBatchId((int)StockType.StockPurchase, a.ProductId),
                    Qty = a.Qty,
                    ReferenceId = savedObj.Id,
                    ReferenceLineId = a.Id,
                    SellPrice = a.SellPrice,
                    TransactionDate = stock.TransactionDate.LocalDateTime.Date,
                    UnitPrice = a.UnitPrice,
                    ReferenceType = (int)StockType.StockPurchase
                });
            });

            await _stockBalanceRepository.InsertStockBalance(stockBalances);

            scope.Complete();

            return _serviceResponseMapper.Map(savedObj);

        }
        public async Task<ServiceResponse> GetStockBySellprice(int id)
        {
            var returnObj = await _stockRepository.GetStockBySellprice(id);
            return _serviceResponseMapper.Map(returnObj);
        }

    }
}
