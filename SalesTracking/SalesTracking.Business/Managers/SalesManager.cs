using Microsoft.EntityFrameworkCore;
using SalesTracking.Common.Common;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Data.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.Common;
using SalesTracking.Entities.Product;
using SalesTracking.Entities.Sales;
using SalesTracking.Entities.Stock;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using static SalesTracking.Common.Constant;

namespace SalesTracking.Business.Managers
{
    public class SalesManager : ISalesManager
    {
        private readonly ISalesRepository _salesRepository;

        private readonly IStockBalanceRepository _stockBalanceRepository;

        private readonly IMapper<Object, ServiceResponse> _serviceResponseMapper;

        private readonly IMapper<IList<Message>, ServiceResponse> _serviceResponseErrorMapper;

        public SalesManager(ISalesRepository salesRepository, IStockBalanceRepository stockBalanceRepository, IMapper<object, ServiceResponse> serviceResponseMapper, IMapper<IList<Message>, ServiceResponse> serviceResponseErrorMapper)
        {
            _salesRepository = salesRepository;
            _stockBalanceRepository = stockBalanceRepository;
            _serviceResponseMapper = serviceResponseMapper;
            _serviceResponseErrorMapper = serviceResponseErrorMapper;
        }

        public ServiceResponse GetSales()
        {
            var returnObj = _salesRepository.GetSales();
            return _serviceResponseMapper.Map(returnObj);
        }

        public ServiceResponse GetSalesById(int id)
        {
            var returnObj = _salesRepository.GetSalesById(id);
            return _serviceResponseMapper.Map(returnObj);
        }

        public ServiceResponse AddSales(SalesDTO sales)
        {
            sales.CreateDate = DateTime.UtcNow;
            sales.CreateBy = UserContext.Current;

            Parallel.ForEach(sales.SalesDetails, a =>
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

            }
            );

            //Note :save payments

            Parallel.ForEach(sales.Payments, a =>
            {
                a.CreateDate = DateTime.UtcNow;
                a.CreateBy = UserContext.Current;

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
                    a.InvoiceNo = sales.InvoiceNo;
                }

            }
            );

            return _serviceResponseMapper.Map(_salesRepository.AddSales(sales));

        }

        public ServiceResponse UpdateSales(SalesDTO sales)
        {
            sales.UpdateDate = DateTime.UtcNow;
            sales.UpdateBy = UserContext.Current;

            Parallel.ForEach(sales.SalesDetails, a =>
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

            //Note : update Payments
            Parallel.ForEach(sales.Payments, a =>
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

            return _serviceResponseMapper.Map(_salesRepository.UpdateSales(sales));
        }

        public ServiceResponse ApproveSales(SalesDTO sales)
        {
            try
            {
                List<ProductDTO> Products = new();
                if (_stockBalanceRepository.CheckStockBalance(sales.SalesDetails, sales.TransactionDate.LocalDateTime.Date, out Products))
                {
                    using var scope = new TransactionScope();
                    if (sales.Id > 0)
                    {
                        sales.UpdateDate = DateTime.UtcNow;
                        sales.UpdateBy = UserContext.Current;
                        sales.ApprovedBy = UserContext.Current;
                        sales.ApprovedDate = DateTime.UtcNow;
                        sales.IsApproved = true;
                    }
                    else
                    {
                        sales.UpdateDate = DateTime.UtcNow;
                        sales.UpdateBy = UserContext.Current;
                        sales.ApprovedBy = UserContext.Current;
                        sales.ApprovedDate = DateTime.UtcNow;
                        sales.IsApproved = true;
                        sales.CreateBy = UserContext.Current;
                        sales.CreateDate = DateTime.UtcNow;
                    }


                    Parallel.ForEach(sales.SalesDetails, a =>
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

                    Parallel.ForEach(sales.Payments, a =>
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

                    var savedObj = _salesRepository.ApproveSales(sales);

                    List<StockBalanceDTO> stockBalances = new();

                    Parallel.ForEach(savedObj.SalesDetails, a =>
                    {

                        stockBalances.Add(new StockBalanceDTO
                        {
                            CreateBy = UserContext.Current,
                            CreateDate = DateTime.UtcNow,
                            IsActive = a.IsActive,
                            ProductId = a.ProductId,
                            Qty = a.Qty,
                            ReferenceId = savedObj.Id,
                            ReferenceLineId = a.Id,
                            SellPrice = a.SellPrice,
                            TransactionDate = sales.TransactionDate.LocalDateTime.Date,
                            UnitPrice = a.UnitPrice,
                            ReferenceType = (int)StockType.Sales
                        });
                    });

                    _stockBalanceRepository.UpdateStockBalance(stockBalances , sales.TransactionDate.LocalDateTime);

                    scope.Complete();

                    return _serviceResponseMapper.Map(savedObj);
                }
                else
                {
                    string productNames = Products.Select(s => s.Name).Aggregate((a, x) => a + ", " + x);

                    List<Message> messages = new()
                    {
                        new Message
                        {
                            Code = "C00001",
                            Description = $"There are insufficient quantities of the following products : {productNames}"
                        }
                    };

                    return _serviceResponseErrorMapper.Map(messages);
                }
            }
            catch
            {
                throw;
            }


        }

    }
}
