using System;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using CurrencyTerminal.Domain.Interfaces;
using CurrencyTerminal.Infrastructure.Repositories;

namespace CurrencyTerminal.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.AddScoped<ICurrencyRepository, CbrCurrencyRepository>();

            return services;
        }
    }
}
