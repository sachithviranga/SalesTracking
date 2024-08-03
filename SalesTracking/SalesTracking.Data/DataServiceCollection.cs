using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SalesTracking.Contracts.Repositories;
using SalesTracking.Data.Repositories;
using SalesTracking.DataContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesTracking.Data
{
    public static class DataServiceCollection
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {

            #region Db Context
            services.AddDbContext<DatabaseContext>(it =>
            {
                it.UseSqlServer(configuration["Database:ConnectionString"]);
            }, ServiceLifetime.Transient);
            #endregion

            #region Repository
            services.AddScoped<IMasterDataRepository, MasterDataRepository>();
            services.AddScoped<ICustomerDataRepository, CustomerDataRepository>();
            services.AddScoped<IProductDataRepository, ProductDataRepository>();
            services.AddScoped<IStockRepository, StockRepository>();
            services.AddScoped<ISalesRepository, SalesRepository > ();
            services.AddScoped<IPaymentsRepository, PaymentsRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRoleDataRepository, RoleDataRepository>();
            services.AddScoped<IStockBalanceRepository, StockBalanceRepository>();
            #endregion

            return services;
        }
    }
}
