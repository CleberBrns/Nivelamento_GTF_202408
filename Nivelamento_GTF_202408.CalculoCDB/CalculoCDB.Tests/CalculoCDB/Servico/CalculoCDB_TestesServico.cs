using Bogus;
using CalculoCDB.CrossCutting.Resources;
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

        private readonly Faker _faker;

        public CalculoCdb_TestesServico()
        {
            _faker = new Faker();

            _impostoFactory = new ImpostoFactory();
            _calculoCDBService = new CalculoCdbService(_impostoFactory);
        }

        [Fact(DisplayName = "Deve Efetuar um Cálculo do CBD com a Faixa de Imposto até Seis Meses.")]
        public void Deve_EfetuarCalculoCdb_ComFaixaImpostoAteSeisMeses()
        {
            //Arrange
            int prazo = _faker.Random.Int(1, 6);
            decimal valor = _faker.Random.Decimal(1m, 10000m);

            //Act
            var resultadoCalculadoInvestimento = _calculoCDBService.CalcularInvestimentoCBD(prazo, valor);

            //Assert
            Assert.True(
                resultadoCalculadoInvestimento.Sucesso &&
                resultadoCalculadoInvestimento.Value != null &&
                resultadoCalculadoInvestimento.Value.ValorFinalBruto > 0 &&
                resultadoCalculadoInvestimento.Value.ValorFinalLiquido > 0);
        }

        [Fact(DisplayName = "Deve Efetuar um Cálculo do CBD com a Faixa de Imposto até Doze Meses.")]
        public void Deve_EfetuarCalculoCdb_ComFaixaImpostoAteDozeMeses()
        {
            //Arrange
            int prazo = _faker.Random.Int(7, 12);
            decimal valor = _faker.Random.Decimal(1m, 10000m);

            //Act
            var resultadoCalculadoInvestimento = _calculoCDBService.CalcularInvestimentoCBD(prazo, valor);

            //Assert
            Assert.True(
                resultadoCalculadoInvestimento.Sucesso &&
                resultadoCalculadoInvestimento.Value != null &&
                resultadoCalculadoInvestimento.Value.ValorFinalBruto > 0 &&
                resultadoCalculadoInvestimento.Value.ValorFinalLiquido > 0);
        }

        [Fact(DisplayName = "Deve Efetuar um Cálculo do CBD com a Faixa de Imposto até Vinte e Quatro Meses.")]
        public void Deve_EfetuarCalculoCdb_ComFaixaImpostoAteVinteQuatroMeses()
        {
            //Arrange
            int prazo = _faker.Random.Int(13, 24);
            decimal valor = _faker.Random.Decimal(1m, 10000m);

            //Act
            var resultadoCalculadoInvestimento = _calculoCDBService.CalcularInvestimentoCBD(prazo, valor);

            //Assert
            Assert.True(
                resultadoCalculadoInvestimento.Sucesso &&
                resultadoCalculadoInvestimento.Value != null &&
                resultadoCalculadoInvestimento.Value.ValorFinalBruto > 0 &&
                resultadoCalculadoInvestimento.Value.ValorFinalLiquido > 0);
        }

        [Fact(DisplayName = "Deve Efetuar um Cálculo do CBD com a Faixa de Imposto maior que Vinte e Quatro Meses.")]
        public void Deve_EfetuarCalculoCdb_ComFaixaImpostoMaiorVinteQuatroMeses()
        {
            //Arrange
            int prazo = _faker.Random.Int(25, 46);
            decimal valor = _faker.Random.Decimal(1m, 10000m);

            //Act
            var resultadoCalculadoInvestimento = _calculoCDBService.CalcularInvestimentoCBD(prazo, valor);

            //Assert
            Assert.True(
                resultadoCalculadoInvestimento.Sucesso &&
                resultadoCalculadoInvestimento.Value != null &&
                resultadoCalculadoInvestimento.Value.ValorFinalBruto > 0 &&
                resultadoCalculadoInvestimento.Value.ValorFinalLiquido > 0);
        }

        [Fact(DisplayName = "Não Deve Efetuar um Cálculo do CBD quando o Prazo informado for Igual a Zero.")]
        public void NaoDeve_EfetuarCalculoCdb_Quando_PrazoForIgualZero()
        {
            //Arrange
            int prazo = 0;
            decimal valor = _faker.Random.Decimal(1m, 10000m);

            var mensagemEsperada = GeneralResource.ValorDoParamentroNaoPodeSerZero(nameof(prazo).ToUpper());

            //Act
            var resultadoCalculadoInvestimento = _calculoCDBService.CalcularInvestimentoCBD(prazo, valor);

            //Assert
            Assert.True(
                !resultadoCalculadoInvestimento.Sucesso &&
                resultadoCalculadoInvestimento.Mensagem.Contains(mensagemEsperada));
        }

        [Fact(DisplayName = "Não Deve Efetuar um Cálculo do CBD quando o Prazo informado for Negativo.")]
        public void NaoDeve_EfetuarCalculoCdb_Quando_PrazoForNegativo()
        {
            //Arrange
            int prazo = -1;
            decimal valor = _faker.Random.Decimal(1m, 10000m);

            var mensagemEsperada = GeneralResource.ValorDoParamentroNaoPodeSerNegativo(nameof(prazo).ToUpper());

            //Act
            var resultadoCalculadoInvestimento = _calculoCDBService.CalcularInvestimentoCBD(prazo, valor);

            //Assert
            Assert.True(
                !resultadoCalculadoInvestimento.Sucesso &&
                resultadoCalculadoInvestimento.Mensagem.Contains(mensagemEsperada));
        }

        [Fact(DisplayName = "Não Deve Efetuar um Cálculo do CBD quando o Valor informado for Igual a Zero.")]
        public void NaoDeve_EfetuarCalculoCdb_Quando_ValorForIgualZero()
        {
            //Arrange
            int prazo = _faker.Random.Int(1, 6);
            decimal valor = 0;

            var mensagemEsperada = GeneralResource.ValorDoParamentroNaoPodeSerZero(nameof(valor).ToUpper());

            //Act
            var resultadoCalculadoInvestimento = _calculoCDBService.CalcularInvestimentoCBD(prazo, valor);

            //Assert
            Assert.True(
                !resultadoCalculadoInvestimento.Sucesso &&
                resultadoCalculadoInvestimento.Mensagem.Contains(mensagemEsperada));
        }

        [Fact(DisplayName = "Não Deve Efetuar um Cálculo do CBD quando o Valor informado for Negativo.")]
        public void NaoDeve_EfetuarCalculoCdb_Quando_ValorForNegativo()
        {
            //Arrange
            int prazo = _faker.Random.Int(1, 6);
            decimal valor = -1;

            var mensagemEsperada = GeneralResource.ValorDoParamentroNaoPodeSerNegativo(nameof(valor).ToUpper());

            //Act
            var resultadoCalculadoInvestimento = _calculoCDBService.CalcularInvestimentoCBD(prazo, valor);

            //Assert
            Assert.True(
                !resultadoCalculadoInvestimento.Sucesso &&
                resultadoCalculadoInvestimento.Mensagem.Contains(mensagemEsperada));
        }
    }
}
