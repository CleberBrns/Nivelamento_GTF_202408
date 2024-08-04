namespace CalculoCDB.Tests.CalculoCDB.Servico
{
    public class CalculoCdb_TestesServico
    {
        private readonly ICalculoCdbService _calculoCDBService;

        public CalculoCdb_TestesServico()
        {
            _calculoCDBService = new CalculoCdbService();
        }

        [Fact(DisplayName = "Deve Efetuar um Calculo do CBD com sucesso")]
        public void DeveEfetuarCalculoCdb_ComSucesso()
        {
            //Arrange

            //Act
            var resultadoCalculadoInvestimento = _calculoCDBService.CalcularInvestimentoCBD(4, 1200m);

            //Assert
            Assert.True(
                resultadoCalculadoInvestimento.Sucesso &&
                resultadoCalculadoInvestimento.Value != null &&
                resultadoCalculadoInvestimento.Value.ValorFinalBruto > 0 &&
                resultadoCalculadoInvestimento.Value.ValorFinalLiquido > 0);
        }
    }

    public class CalculoCdbService : ICalculoCdbService
    {
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
            decimal valorFinalLiquido = 0;

            for (int i = 0; i < prazo; i++)
            {
                valorFinalBruto *= EfetuarCalculoCBD();
                valorFinalLiquido += valorFinalBruto;
            }

            var investimentoCalculado = new InvestimentoCalculado(valorFinalBruto, valorFinalLiquido);

            return new Resultado<InvestimentoCalculado>(investimentoCalculado, true, string.Empty);
        }

        static decimal EfetuarCalculoCBD()
        {
            decimal porcentagemCDI = 0.9m;
            decimal procentagemTB = 108m;

            return 1 + (PercentualTaxa(porcentagemCDI) * PercentualTaxa(procentagemTB));
        }

        static decimal PercentualTaxa(decimal taxa)
        {
            return taxa / 100;
        }
    }

    public interface ICalculoCdbService
    {
        /// <summary>
        /// Efetua o Cáculo para um Investimento CBD
        /// </summary>
        /// <param name="prazo"></param>
        /// <param name="valorInvestir"></param>
        /// <returns>ValorFinalBruto, ValorFinalLiquido</returns>
        Resultado<InvestimentoCalculado> CalcularInvestimentoCBD(int prazo, decimal valorInvestir);
    }

    public class Resultado<T>
    {
        public T Value { get; set; }

        public bool Sucesso { get; set; }

        public string Mensagem { get; private set; }

        public Resultado(T value, bool sucesso, string mensagem)
        {
            Value = value;
            Sucesso = sucesso;
            Mensagem = mensagem;
        }
    }

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
