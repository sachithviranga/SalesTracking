using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesTracking.Business.Managers;
using SalesTracking.Common.Mappers;
using SalesTracking.Contracts.Common;
using SalesTracking.Contracts.Managers;
using SalesTracking.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Business
{
    public static class BusinessServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {

            #region Manager
            services.AddScoped<ICustomerDataManager, CustomerDataManager>();
            services.AddScoped<IProductDataManager, ProductDataManager>();
            services.AddScoped<IMasterDataManager, MasterDataManager>();
            services.AddScoped<IStockManager, StockManager>();
            services.AddScoped<ISalesManager, SalesManager>();
            services.AddScoped<IPaymentsManager, PaymentsManager>();
            services.AddScoped<IAuthManager, AuthManager>();
            services.AddScoped<IUserDataManager, UserDataManager>();
            services.AddScoped<IRoleDataManager, RoleDataManager>();
            services.AddScoped<IDashboardManager, DashboardManager>();
            #endregion

            #region Mapper
            services.AddSingleton<IMapper<Object, ServiceResponse>, ServiceResponseMapper>();
            services.AddSingleton<IMapper<IList<Message>, ServiceResponse>, ServiceErrorMapper>();
            #endregion


            return services;
        }
    }
}
