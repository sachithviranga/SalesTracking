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

        public List<PaymentDTO> GetPayments()
        {
            try
            {
                var payments = _context.Payments
                            .Where(a => a.IsActive == true).ToList();

                return _mapper.Map<List<PaymentDTO>>(payments);
            }
            catch
            {
                throw;
            }


        }

        public int AddPayments(PaymentDTO payments)
        {
            try
            {
                var saveObj = _mapper.Map<Payments>(payments);
                _context.Payments.Add(saveObj);
                _context.SaveChanges();
                return saveObj.Id;
            }
            catch
            {
                throw;

            }

        }

        public PaymentDTO UpdatePayments(PaymentDTO payments)
        {
            try
            {
                var updateObj = _context.Payments.FirstOrDefault(a => a.Id == payments.Id);
                if (updateObj != null)
                {
                    updateObj.InvoiceNo = payments.InvoiceNo;
                    updateObj.ChequeNo = payments.ChequeNo;
                    updateObj.PaymentTypeId = payments.PaymentTypeId;
                    updateObj.IsActive = payments.IsActive;
                    updateObj.UpdateBy = payments.UpdateBy;
                    updateObj.UpdateDate = payments.UpdateDate;
                    _context.SaveChanges();
                }
                return _mapper.Map<PaymentDTO>(updateObj);
            }
            catch
            {
                throw;

            }
        }
    }
}
