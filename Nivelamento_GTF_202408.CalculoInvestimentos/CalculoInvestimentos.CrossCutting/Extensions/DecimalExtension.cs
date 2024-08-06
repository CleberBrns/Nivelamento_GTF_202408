using Investimentos.CrossCutting.Resources;
using Investimentos.CrossCutting.Validadores;

namespace Investimentos.CrossCutting.Extensions
{
    public static class DecimalExtension
    {
        /// <summary>
        /// Retorna a Porcentagem de uma Taxa
        /// </summary>
        /// <param name="taxa"></param>
        /// <returns></returns>
        public static decimal PercentualTaxa(this decimal taxa)
        {
            ValidadorException.Validar(taxa == 0, GeneralResource.ValorDoParamentroNaoPodeSerZero(nameof(taxa).ToUpper()));

            return taxa / 100;
        }

        /// <summary>
        /// Cálcula o Imposto sobre um Valor Bruto e retorna o Valor Liquído
        /// </summary>
        /// <param name="valorBruto"></param>
        /// <param name="imposto"></param>
        /// <returns></returns>
        public static decimal CalcularValorComImposto(this decimal valorBruto, decimal imposto)
        {
            ValidadorRegra.New()
                .Quando(valorBruto == 0, GeneralResource.ValorDoParamentroNaoPodeSerZero(nameof(valorBruto).ToUpper()))
                .Quando(imposto == 0, GeneralResource.ValorDoParamentroNaoPodeSerZero(nameof(imposto).ToUpper()))
                .ThrowMensagensValidacao();

            decimal valorDesconto = valorBruto * imposto.PercentualTaxa();
            decimal valorLiquido = valorBruto - valorDesconto;

            return valorLiquido;
        }
    }
}
