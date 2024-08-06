namespace Investimentos.Service.Interfaces.Imposto
{
    public interface IImpostoFactory
    {
        /// <summary>
        /// Retorna um Calculador de Imposto de acordo com a Quantidade de Meses Informada
        /// </summary>
        /// <param name="quantidadeMeses"></param>
        /// <returns></returns>
        ICalculadorImposto GetCalculadorImposto(int quantidadeMeses);
    }
}
