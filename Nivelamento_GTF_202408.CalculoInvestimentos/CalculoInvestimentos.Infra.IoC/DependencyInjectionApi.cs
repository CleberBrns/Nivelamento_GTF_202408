using Investimentos.Service.Interfaces.CalculoCdb;
using Investimentos.Service.Interfaces.Imposto;
using Investimentos.Service.Services.CalculoCdb;
using Investimentos.Service.Services.Imposto;
using Microsoft.Extensions.DependencyInjection;

namespace CalculoInvestimentos.Infra.IoC
{
    public static class DependencyInjectionApi
    {
        public static IServiceCollection AddInfrastructureApi(this IServiceCollection services)
        {
            services.AddScoped<ICalculoCdbService, CalculoCdbService>();
            services.AddScoped<IImpostoFactory, ImpostoFactory>();

            return services;
        }
    }
}
