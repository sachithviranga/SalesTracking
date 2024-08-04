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

        public async Task<List<SalesDTO>> GetSales()
        {
            var sales = await _context.Sales
                        .Where(a => a.IsActive == true)
                        .Include(i => i.SalesDetails).Include(a => a.Payments)
                        .ToListAsync();

            return _mapper.Map<List<SalesDTO>>(sales);
        }

        public async Task<SalesDTO> GetSalesById(int id)
        {
            var sales = await _context.Sales.Where(a => a.Id == id)
                .Include(i => i.SalesDetails).ThenInclude(c => c.Product)
                .Include(i => i.SalesDetails).ThenInclude(c => c.Price)
                .Include(h => h.Payments).ThenInclude(d => d.PaymentType).SingleOrDefaultAsync();

            return _mapper.Map<SalesDTO>(sales);
        }

        public async Task<int> AddSales(SalesDTO sales)
        {
            var saveObj = _mapper.Map<Sales>(sales);
            await _context.Sales.AddAsync(saveObj);
            await _context.SaveChangesAsync();
            return saveObj.Id;
        }

        public async Task<SalesDTO> UpdateSales(SalesDTO sales)
        {

            var updateObj = await _context.Sales.FirstOrDefaultAsync(a => a.Id == sales.Id);
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
                updateObj.TotalAmout = sales.TotalAmout;
                updateObj.TotalPayment = sales.TotalPayment;
                updateObj.InvoiceNo = sales.InvoiceNo;
                updateObj.IsActive = sales.IsActive;
                updateObj.UpdateBy = sales.UpdateBy;
                updateObj.UpdateDate = sales.UpdateDate;

                if (sales.SalesDetails.Any())
                {
                    var salesDetails = _mapper.Map<List<SalesDetails>>(sales.SalesDetails);
                    await _context.SalesDetails.AddRangeAsync(salesDetails);//455555
                    foreach (var saleDetail in salesDetails)
                    {
                        updateObj.SalesDetails.Add(saleDetail);
                    }
                }

                if (sales.Payments.Any())
                {
                    var salespayments = _mapper.Map<List<Payments>>(sales.Payments);
                    await _context.Payments.AddRangeAsync(salespayments);
                    foreach (var payments in salespayments)
                    {
                        updateObj.Payments.Add(payments);
                    }
                }
                _context.Sales.Update(updateObj);
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<SalesDTO>(updateObj);
        }

        public async Task<SalesDTO> ApproveSales(SalesDTO sales)
        {

            var appObj = await _context.Sales.FirstOrDefaultAsync(a => a.Id == sales.Id);

            if (appObj != null)
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
                appObj.TotalAmout = sales.TotalAmout;
                appObj.InvoiceNo = sales.InvoiceNo;
                appObj.IsActive = sales.IsActive;
                appObj.UpdateBy = sales.UpdateBy;
                appObj.UpdateDate = sales.UpdateDate;
                appObj.ApprovedBy = sales.ApprovedBy;
                appObj.ApprovedDate = sales.ApprovedDate;
                appObj.IsApproved = sales.IsApproved;


                if (sales.SalesDetails.Any())
                {
                    var salesDetails = _mapper.Map<List<SalesDetails>>(sales.SalesDetails);
                    await _context.SalesDetails.AddRangeAsync(salesDetails);
                    foreach (var saleDetail in salesDetails)
                    {
                        appObj.SalesDetails.Add(saleDetail);
                    }

                }


                if (sales.Payments.Any())
                {
                    var salespayments = _mapper.Map<List<Payments>>(sales.Payments);
                    await _context.Payments.AddRangeAsync(salespayments);
                    foreach (var payments in salespayments)
                    {
                        appObj.Payments.Add(payments);
                    }
                }


                _context.Sales.Update(appObj);
                await _context.SaveChangesAsync();
                return _mapper.Map<SalesDTO>(appObj);
            }
            else
            {

                var saveObj = _mapper.Map<Sales>(sales);
                await _context.Sales.AddRangeAsync(saveObj);
                await _context.SaveChangesAsync();
                return _mapper.Map<SalesDTO>(saveObj);
            }
        }
    }
}
