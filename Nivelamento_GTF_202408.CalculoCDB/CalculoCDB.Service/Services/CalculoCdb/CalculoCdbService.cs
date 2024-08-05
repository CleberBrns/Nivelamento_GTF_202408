using CalculoCDB.CrossCutting.Generics;
using CalculoCDB.Domain.Models.CalculoCDB;
using CalculoCDB.Service.Interfaces.CalculoCdb;
using CalculoCDB.Service.Interfaces.Imposto;
using CalculoCDB.CrossCutting.Extensions;

namespace CalculoCDB.Service.Services.CalculoCdb
{
    public class CalculoCdbService : ICalculoCdbService
    {
        private readonly IImpostoFactory _impostoFactory;

        public CalculoCdbService(IImpostoFactory impostoFactory)
        {
            _impostoFactory = impostoFactory;
        }

        /// <summary>
        /// Efetua o Cáculo para um Investimento CBD
        /// </summary>
        /// <param name="prazo">Prazo do Investimento</param>
        /// <param name="valor">Valor do Investimento</param>
        /// <returns>ValorFinalBruto, ValorFinalLiquido</returns>
        public Resultado<InvestimentoCalculado> CalcularInvestimentoCBD(int prazo, decimal valor)
        {
            decimal valorFinalBruto = valor;

            for (int i = 0; i < prazo; i++)
            {
                valorFinalBruto *= EfetuarCalculoCBD();
            }

            ICalculadorImposto calculadorImposto = _impostoFactory.GetCalculadorImposto(prazo);

            decimal valorFinalLiquido = calculadorImposto.CalcularImposto(valorFinalBruto);
            var investimentoCalculado = new InvestimentoCalculado(valorFinalBruto, valorFinalLiquido);

            return new Resultado<InvestimentoCalculado>(investimentoCalculado, true, string.Empty);
        }

        static decimal EfetuarCalculoCBD()
        {
            decimal taxaCDI = 0.9m;
            decimal taxaTB = 108m;

            return 1 + (taxaCDI.PercentualTaxa() * taxaTB.PercentualTaxa());
        }
    }
}
