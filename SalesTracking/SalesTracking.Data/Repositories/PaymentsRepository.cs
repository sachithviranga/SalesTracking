using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesTracking.Contracts.Repositories;
using SalesTracking.DataContext;
using SalesTracking.Entities.Payment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Data.Repositories
{
    public class PaymentsRepository : IPaymentsRepository
    {
        private readonly DatabaseContext _context;

        private readonly IMapper _mapper;

        public PaymentsRepository(DatabaseContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<PaymentDTO>> GetPayments()
        {

            var payments = await _context.Payments
                                .Where(a => a.IsActive == true).ToListAsync();

            return _mapper.Map<List<PaymentDTO>>(payments);
        }

        public async Task<int> AddPayments(PaymentDTO payments)
        {
            var saveObj = _mapper.Map<Payments>(payments);
            await _context.Payments.AddAsync(saveObj);
            await _context.SaveChangesAsync();
            return saveObj.Id;
        }

        public async Task<PaymentDTO> UpdatePayments(PaymentDTO payments)
        {

            var updateObj = await _context.Payments.FirstOrDefaultAsync(a => a.Id == payments.Id);
            if (updateObj != null)
            {
                updateObj.InvoiceNo = payments.InvoiceNo;
                updateObj.ChequeNo = payments.ChequeNo;
                updateObj.PaymentTypeId = payments.PaymentTypeId;
                updateObj.IsActive = payments.IsActive;
                updateObj.UpdateBy = payments.UpdateBy;
                updateObj.UpdateDate = payments.UpdateDate;
                await _context.SaveChangesAsync();
            }
            return _mapper.Map<PaymentDTO>(updateObj);
        }
    }
}
