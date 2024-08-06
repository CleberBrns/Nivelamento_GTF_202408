namespace Investimentos.Domain.Models.CalculoCDB
{
    public class InvestimentoCalculado
    {
        public decimal ValorFinalBruto { get; private set; }

        public decimal ValorFinalLiquido { get; private set; }

        public InvestimentoCalculado(decimal valorFinalBruto, decimal valorFinalLiquido)
        {
            ValorFinalBruto = valorFinalBruto;
            ValorFinalLiquido = valorFinalLiquido;
        }
    }
}
