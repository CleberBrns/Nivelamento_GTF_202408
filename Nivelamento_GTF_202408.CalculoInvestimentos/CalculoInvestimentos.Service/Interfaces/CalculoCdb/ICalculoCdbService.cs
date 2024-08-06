using Investimentos.CrossCutting.Generics;
using Investimentos.Domain.Models.CalculoCDB;

namespace Investimentos.Service.Interfaces.CalculoCdb
{
    public interface ICalculoCdbService
    {
        Resultado<InvestimentoCalculado> CalcularInvestimentoCdb(int prazo, decimal valor);
    }
}
