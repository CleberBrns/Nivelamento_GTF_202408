namespace Investimentos.Service.Interfaces.Imposto
{
    public interface ICalculadorImposto
    {
        /// <summary>
        /// Assinatura de Método para Calcular Diferentes Tipos De Impostos
        /// </summary>
        /// <param name="valorBruto"></param>
        /// <returns></returns>
        decimal CalcularImposto(decimal valorBruto);
    }
}
