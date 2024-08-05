using CalculoCDB.CrossCutting.Generics;
using CalculoCDB.Domain.Models.CalculoCDB;

namespace CalculoCDB.Service.Interfaces.CalculoCdb
{
    public interface ICalculoCdbService
    {
        /// <summary>
        /// Efetua o Cáculo para um Investimento CBD
        /// </summary>
        /// <param name="prazo"></param>
        /// <param name="valorInvestir"></param>
        /// <returns>ValorFinalBruto, ValorFinalLiquido</returns>
        Resultado<InvestimentoCalculado> CalcularInvestimentoCBD(int prazo, decimal valorInvestir);
    }
}
