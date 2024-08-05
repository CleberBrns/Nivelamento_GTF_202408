using CalculoCDB.Service.Interfaces.Imposto;

namespace CalculoCDB.Service.Services.Imposto
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
            if (quantidadeMeses == 0)
                throw new ArgumentException("Quantidade de Meses não pode ser igual a Zero!");

            return quantidadeMeses switch
            {
                var _ when quantidadeMeses <= 6 => new ImpostoAteSeisMeses(),
                var _ when quantidadeMeses is > 6 and <= 12 => new ImpostoAteDozeMeses(),
                var _ when quantidadeMeses is > 12 and <= 24 => new ImpostoAteVinteQuatroMeses(),
                var _ when quantidadeMeses > 24 => new ImpostoAcimaDeVinteQuatroMeses(),
                _ => null,
            };
        }
    }
}
