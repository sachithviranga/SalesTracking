using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesTracking.Contracts.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.Product;
using SalesTracking.Entities.Sales;
using SalesTracking.Entities.Stock;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Data.Repositories
{
    public class SalesRepository : ISalesRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public SalesRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public List<SalesDTO> GetSales()
        {
            try
            {
                var sales = _context.Sales
                            .Where(a => a.IsActive == true)
                            .Include(i => i.SalesDetails).Include(a => a.Payments)
                            .ToList();

                return _mapper.Map<List<SalesDTO>>(sales);
            }
            catch
            {
                throw;
            }


        }

        public SalesDTO GetSalesById(int id)
        {
            try
            {
                var sales = _context.Sales.Where(a => a.Id == id)
                    .Include(i => i.SalesDetails).ThenInclude(c => c.Product)
                    .Include(i => i.SalesDetails).ThenInclude(c => c.Price)
                    .Include(h => h.Payments).ThenInclude(d => d.PaymentType).SingleOrDefault();

                return _mapper.Map<SalesDTO>(sales);
            }
            catch
            {
                throw;
            }
        }

        public int AddSales(SalesDTO sales)
        {
            try
            {
                var saveObj = _mapper.Map<Sales>(sales);
                _context.Sales.Add(saveObj);
                _context.SaveChanges();
                return saveObj.Id;
            }
            catch
            {
                throw;

            }

        }

        public SalesDTO UpdateSales(SalesDTO sales)
        {
            try
            {
                var updateObj = _context.Sales.FirstOrDefault(a => a.Id == sales.Id);



                if (updateObj != null)
                {
                    _context.Entry(updateObj).Collection(l => l.SalesDetails).Load();

                    if (updateObj.SalesDetails.Any())
                    {
                        _context.RemoveRange(updateObj.SalesDetails);
                    }

                    _context.Entry(updateObj).Collection(l => l.Payments).Load();
                    if (updateObj.Payments.Any())
                    {
                        _context.RemoveRange(updateObj.Payments);
                    }

                    updateObj.CustomerId = sales.CustomerId;
                    updateObj.TotalAmout=sales.TotalAmout;
                    updateObj.TotalPayment=sales.TotalPayment;
                    updateObj.InvoiceNo = sales.InvoiceNo;
                    updateObj.IsActive = sales.IsActive;
                    updateObj.UpdateBy = sales.UpdateBy;
                    updateObj.UpdateDate = sales.UpdateDate;

                    if (sales.SalesDetails.Any())
                    {
                        var salesDetails = _mapper.Map<List<SalesDetails>>(sales.SalesDetails);
                        _context.SalesDetails.AddRange(salesDetails);//455555
                        foreach (var saleDetail in salesDetails)
                        {
                            updateObj.SalesDetails.Add(saleDetail);
                        }
                    }

                    if (sales.Payments.Any())
                    {
                        var salespayments = _mapper.Map<List<Payments>>(sales.Payments);
                        _context.Payments.AddRange(salespayments);
                        foreach (var payments in salespayments)
                        {
                            updateObj.Payments.Add(payments);
                        }
                    }
                    _context.Sales.Update(updateObj);
                    _context.SaveChanges();
                }
                return _mapper.Map<SalesDTO>(updateObj);
            }
            catch
            {
                throw;

            }
        }

        public SalesDTO ApproveSales(SalesDTO sales)
        {
            try
            {
                var appObj = _context.Sales.FirstOrDefault(a=> a.Id==sales.Id);

                if (appObj !=null)
                {
                
                    _context.Entry(appObj).Collection(l => l.SalesDetails).Load();

                    if (appObj.SalesDetails.Any()) 
                    {
                        _context.RemoveRange(appObj.SalesDetails);
                    }

                    _context.Entry(appObj).Collection(l => l.Payments).Load();
                    if (appObj.Payments.Any())
                    {
                        _context.RemoveRange(appObj.Payments);
                    }

                    appObj.CustomerId = sales.CustomerId;
                    appObj.TotalPayment = sales.TotalPayment;
                    appObj.TotalAmout=sales.TotalAmout;
                    appObj.InvoiceNo = sales.InvoiceNo;
                    appObj.IsActive = sales.IsActive;
                    appObj.UpdateBy = sales.UpdateBy;
                    appObj.UpdateDate = sales.UpdateDate;
                    appObj.ApprovedBy= sales.ApprovedBy;
                    appObj.ApprovedDate = sales.ApprovedDate;
                    appObj.IsApproved = sales.IsApproved;


                    if (sales.SalesDetails.Any())
                    {
                        var salesDetails = _mapper.Map<List<SalesDetails>>(sales.SalesDetails);
                        _context.SalesDetails.AddRange(salesDetails);
                        foreach (var saleDetail in salesDetails)
                        {
                            appObj.SalesDetails.Add(saleDetail);
                        }

                    }
                   

                    if (sales.Payments.Any())
                    {
                        var salespayments = _mapper.Map<List<Payments>>(sales.Payments);
                        _context.Payments.AddRange(salespayments);
                        foreach (var payments in salespayments)
                        {
                            appObj.Payments.Add(payments);
                        }
                    }

                    //{
                    //    //note : upsate stock balance
                    //    int prodId = saleDetail.ProductId;
                    //    int prodQty = saleDetail.Qty;
                    //    int balQty = prodQty;
                    //    var getStockObj = _context.StockBalance.Where(a => a.ProductId == prodId && a.Qty>0).ToList();
                    //    if (getStockObj != null)
                    //    {

                    //        foreach (var stockBal in getStockObj)
                    //        {
                    //            if (balQty > 0) {
                    //                int stockId = stockBal.Id;
                    //                int stkQty = stockBal.Qty;
                    //                var batchId = stockBal.BatchId;


                    //                if (stkQty > balQty)
                    //                {
                    //                    //var updStockObj1 = _context.StockBalance.FirstOrDefault((a => a.Id == stockId));
                    //                    //updStockObj1.Qty = updStockObj1.Qty - balQty;
                    //                    balQty = 0;
                    //                }
                    //                else
                    //                {
                    //                    var updStockObj = _context.StockBalance.FirstOrDefault((a => a.Id == stockId));
                    //                    updStockObj.Qty = updStockObj.Qty - stkQty;

                    //                    balQty = prodQty - stkQty;
                    //                }
                    //            }                                                                    
                    //        }

                    //    }



                    //var getpriceObj = _context.ProductPrice.FirstOrDefault(a => a.ProductId == product.Id);
                    _context.Sales.Update(appObj);
                    _context.SaveChanges();
                    return _mapper.Map<SalesDTO>(appObj);
                }
                                                                     
                else
                {
                    
                  var saveObj = _mapper.Map<Sales>(sales);
                   _context.Sales.Add(saveObj);
                   _context.SaveChanges();
                   return _mapper.Map<SalesDTO>(saveObj);
                }
                
            }
            catch 
            {
                throw;

            }

        }
    }
}
