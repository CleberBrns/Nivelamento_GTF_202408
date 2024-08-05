using CalculoCDB.CrossCutting.Generics;
using CalculoCDB.Domain.Models.CalculoCDB;

namespace CalculoCDB.Service.Interfaces.CalculoCdb
{
    public interface ICalculoCdbService
    {
        Resultado<InvestimentoCalculado> CalcularInvestimentoCBD(int prazo, decimal valor);
    }
}
