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
        /// <param name="prazo"></param>
        /// <param name="valorInvestir"></param>
        /// <returns>Retorna o Resultado do Cálculo de um investimento, contendo o Valor Final Bruto e o Valor Final Líquido</returns>
        /// <exception cref="NotImplementedException"></exception>
        public Resultado<InvestimentoCalculado> CalcularInvestimentoCBD(int prazo, decimal valorInvestir)
        {
            decimal valorFinalBruto = valorInvestir;

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
