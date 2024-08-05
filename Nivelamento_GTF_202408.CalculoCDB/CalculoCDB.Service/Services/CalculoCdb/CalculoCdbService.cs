using CalculoCDB.CrossCutting.Generics;
using CalculoCDB.Domain.Models.CalculoCDB;
using CalculoCDB.Service.Interfaces.CalculoCdb;
using CalculoCDB.Service.Interfaces.Imposto;
using CalculoCDB.CrossCutting.Extensions;
using CalculoCDB.CrossCutting.Resources;
using CalculoCDB.CrossCutting.Validadores;

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
            try
            {
                ValidadorRegra.New()
               .Quando(prazo == 0, GeneralResource.ValorDoParamentroNaoPodeSerZero(nameof(prazo).ToUpper()))
               .Quando(prazo < 0, GeneralResource.ValorDoParamentroNaoPodeSerNegativo(nameof(prazo).ToUpper()))
               .Quando(valor == 0, GeneralResource.ValorDoParamentroNaoPodeSerZero(nameof(valor).ToUpper()))
               .Quando(valor < 0, GeneralResource.ValorDoParamentroNaoPodeSerNegativo(nameof(valor).ToUpper()))
               .ThrowMensagensValidacao();

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
            catch (ValidadorException vex)
            {
                return new Resultado<InvestimentoCalculado>(null, false, vex.ResumoMensagensValidacao());
            }
            catch (Exception ex)
            {
                // Logar Exception
                return new Resultado<InvestimentoCalculado>(null, false, ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        static decimal EfetuarCalculoCBD()
        {
            decimal taxaCDI = 0.9m;
            decimal taxaTB = 108m;

            return 1 + (taxaCDI.PercentualTaxa() * taxaTB.PercentualTaxa());
        }
    }
}
