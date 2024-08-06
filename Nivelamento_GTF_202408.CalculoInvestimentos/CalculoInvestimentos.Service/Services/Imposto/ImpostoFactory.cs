using CalculoInvestimentos.CrossCutting.Resources;
using Investimentos.CrossCutting.Resources;
using Investimentos.CrossCutting.Validadores;
using Investimentos.Service.Interfaces.Imposto;

namespace Investimentos.Service.Services.Imposto
{
    public class ImpostoFactory : IImpostoFactory
    {
        /// <summary>
        /// Retorna um Calculador de Imposto de acordo com a Quantidade de Meses Informada
        /// </summary>
        /// <param name="quantidadeMeses"></param>
        /// <returns></returns>
        public ICalculadorImposto GetCalculadorImposto(int quantidadeMeses)
        {
            ValidadorRegra.New()
                   .Quando(quantidadeMeses == 0, GeneralResource.ValorDoParamentroNaoPodeSerZero(nameof(quantidadeMeses).ToUpper()))
                   .Quando(quantidadeMeses < 0, GeneralResource.ValorDoParamentroNaoPodeSerNegativo(nameof(quantidadeMeses).ToUpper()))
                   .ThrowMensagensValidacao();

            ICalculadorImposto calculadorImposto = quantidadeMeses switch
            {
                var _ when quantidadeMeses <= 6 => new ImpostoAteSeisMeses(),
                var _ when quantidadeMeses is > 6 and <= 12 => new ImpostoAteDozeMeses(),
                var _ when quantidadeMeses is > 12 and <= 24 => new ImpostoAteVinteQuatroMeses(),
                var _ when quantidadeMeses > 24 => new ImpostoAcimaDeVinteQuatroMeses(),
                _ => null,
            };

            ValidadorException.Validar(calculadorImposto == null,
                ImpostoResource.ImpostoNaoParametrizadoParaQuantidadeMeses);

            return calculadorImposto;
        }
    }
}
