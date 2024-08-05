namespace CalculoCDB.CrossCutting.Extensions
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
            if (taxa == 0)
                throw new ArgumentException("Valor da Taxa não pode ser igual a Zero!");

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
            if (valorBruto == 0)
                throw new ArgumentException("Valor Bruto não pode ser igual a Zero!");

            if (imposto == 0)
                throw new ArgumentException("Valor do Imposto não pode ser igual a Zero!");

            decimal valorDesconto = valorBruto * imposto.PercentualTaxa();
            decimal valorLiquido = valorBruto - valorDesconto;

            return valorLiquido;
        }
    }
}
