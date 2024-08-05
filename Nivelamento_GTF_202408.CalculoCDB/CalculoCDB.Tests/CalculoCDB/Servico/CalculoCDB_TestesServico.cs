using CalculoCDB.Service.Interfaces.CalculoCdb;
using CalculoCDB.Service.Interfaces.Imposto;
using CalculoCDB.Service.Services.CalculoCdb;
using CalculoCDB.Service.Services.Imposto;

namespace CalculoCDB.Tests.CalculoCDB.Servico
{
    public class CalculoCdb_TestesServico
    {
        private readonly ICalculoCdbService _calculoCDBService;

        private readonly IImpostoFactory _impostoFactory;

        public CalculoCdb_TestesServico()
        {
            _impostoFactory = new ImpostoFactory();
            _calculoCDBService = new CalculoCdbService(_impostoFactory);
        }

        [Fact(DisplayName = "Deve Efetuar um Calculo do CBD com sucesso")]
        public void DeveEfetuarCalculoCdb_ComSucesso()
        {
            //Arrange

            //Act
            var resultadoCalculadoInvestimento = _calculoCDBService.CalcularInvestimentoCBD(7, 1200m);

            //Assert
            Assert.True(
                resultadoCalculadoInvestimento.Sucesso &&
                resultadoCalculadoInvestimento.Value != null &&
                resultadoCalculadoInvestimento.Value.ValorFinalBruto > 0 &&
                resultadoCalculadoInvestimento.Value.ValorFinalLiquido > 0);
        }
    }
}
