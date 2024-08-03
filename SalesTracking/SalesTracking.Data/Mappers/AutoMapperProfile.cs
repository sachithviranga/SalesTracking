using AutoMapper;
using SalesTracking.DataContext;
using SalesTracking.Entities.Auth;
using SalesTracking.Entities.Customer;
using SalesTracking.Entities.MasterData;
using SalesTracking.Entities.Payment;
using SalesTracking.Entities.Product;
using SalesTracking.Entities.Sales;
using SalesTracking.Entities.Stock;
using SalesTracking.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Module = SalesTracking.DataContext.Module;

namespace SalesTracking.Data.Mappers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<CustomerType, CustomerTypeDTO>().ReverseMap();
            CreateMap<PaymentType, PaymentTypeDTO>().ReverseMap();
            CreateMap<Customer, CustomerDTO>().ForMember(m => m.CustomerTypeName, a => a.MapFrom(u => u.CustomerType.Name));
            CreateMap<CustomerDTO, Customer>();
            CreateMap<Product, ProductDTO>()
                .ForMember(m => m.ProductPrice, a => a.MapFrom(i => i.ProductPrice.Where(a => a.IsCurrent == true)))
                .ForMember(m => m.SellPrice, a => a.MapFrom(i => i.ProductPrice.FirstOrDefault(a => a.IsCurrent == true).SellPrice))
                .ForMember(m => m.UnitPrice, a => a.MapFrom(i => i.ProductPrice.FirstOrDefault(a => a.IsCurrent == true).UnitPrice));
            CreateMap<ProductDTO, Product>();
            CreateMap<ProductPrice, ProductPriceDTO>().ReverseMap();
            CreateMap<StockPurchase, StockPurchaseDTO>();
            CreateMap<StockPurchaseDTO ,StockPurchase>().ForMember(a => a.TransactionDate, m => m.MapFrom(d => d.TransactionDate.LocalDateTime.Date));
            CreateMap<StockPurchaseDetails, StockPurchaseDetailsDTO>().ForMember(a => a.ProductName, a => a.MapFrom(u => u.Product.Name));
            CreateMap<StockPurchaseDetailsDTO, StockPurchaseDetails>();
            CreateMap<StockPurchasePayment, StockPurchasePaymentDTO>().ForMember(a => a.PaymentTypeName, a => a.MapFrom(u => u.PaymentType.Name));
            CreateMap<StockPurchasePaymentDTO, StockPurchasePayment>();
            CreateMap<Sales, SalesDTO>().ForMember(a => a.CustomerName, a => a.MapFrom(b => b.Customer.Name));
            CreateMap<SalesDTO, Sales>().ForMember(a => a.TransactionDate, m => m.MapFrom(d => d.TransactionDate.LocalDateTime.Date)); ;
            CreateMap<SalesDetails, SalesDetailsDTO>().ForMember(a => a.ProductName, a => a.MapFrom(b => b.Product.Name));
            CreateMap<SalesDetails, SalesDetailsDTO>().ForMember(a => a.SellPrice, a => a.MapFrom(b => b.Price.SellPrice));
            CreateMap<SalesDetailsDTO, SalesDetails>();
            CreateMap<Payments, PaymentsDTO>().ForMember(a => a.PaymentTypeName, a => a.MapFrom(b => b.PaymentType.Name));
            CreateMap<PaymentsDTO, Payments>();
            CreateMap<User, UserDTO>().ForMember(a => a.Password, m => m.MapFrom(u => string.Empty));
            CreateMap<UserDTO, User>().ReverseMap();
            CreateMap<Module, ModuleDTO>().ReverseMap();
            CreateMap<Role, RoleDTO>().ReverseMap();
            CreateMap<RoleClaim, RoleClaimDTO>().ReverseMap();
            CreateMap<Claim, ClaimDTO>().ReverseMap();
            CreateMap<UserRole, UserRoleDTO>().ReverseMap();
            CreateMap<StockBalance, StockBalanceDTO>().ForMember(a => a.ProductName, a => a.MapFrom(u => u.Product.Name));
            CreateMap<StockBalanceDTO, StockBalance>();
        }
    }
}
