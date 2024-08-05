using System.Collections.Generic;
using System.ComponentModel;

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

    public class CalculoCdbService : ICalculoCdbService
    {
        private readonly IImpostoFactory _impostoFactory;

        public CalculoCdbService(IImpostoFactory impostoFactory)
        {
            _impostoFactory = impostoFactory;
        }

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
            }

            ICalculadorImposto calculadorImposto = _impostoFactory.GetCalculadorImposto(prazo);
            valorFinalLiquido = calculadorImposto.CalcularImposto(valorFinalBruto);

            var investimentoCalculado = new InvestimentoCalculado(valorFinalBruto, valorFinalLiquido);

            return new Resultado<InvestimentoCalculado>(investimentoCalculado, true, string.Empty);
        }

        static decimal EfetuarCalculoCBD()
        {
            decimal taxaCDI = 0.9m;
            decimal taxaTB = 108m;

            return 1 + (taxaCDI.PercentualTaxa() * taxaTB.PercentualTaxa());
        }
    }

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

    public interface IImpostoFactory
    {
        ICalculadorImposto GetCalculadorImposto(int quantidadeMeses);
    }

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
                _ => throw new ArgumentException("Imposto não configurado para a Quantidade de Meses informada."),
            };
        }
    }

    public interface ICalculadorImposto
    {
        /// <summary>
        /// Assinatura de Método para Calcular Diferentes Tipos De Impostos
        /// </summary>
        /// <param name="valorBruto"></param>
        /// <returns></returns>
        decimal CalcularImposto(decimal valorBruto);
    }

    public class ImpostoAteSeisMeses : ICalculadorImposto
    {
        private readonly decimal _imposto;

        public ImpostoAteSeisMeses()
        {
            // Definido no Construtor para fins de futuras implemenções na definição do valor no AppSettings ou XML
            // Imposto Até 06 meses: 22,5%
            _imposto = 22.5m;
        }

        /// <summary>
        /// Cáclula o Imposto para investimentos de até 06 meses
        /// </summary>
        /// <param name="valorBruto"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public decimal CalcularImposto(decimal valorBruto)
        {
            decimal valorDesconto = valorBruto * _imposto.PercentualTaxa();
            decimal valorLiquido = valorBruto - valorDesconto;

            return valorLiquido;
        }
    }

    public class ImpostoAteDozeMeses : ICalculadorImposto
    {
        private readonly decimal _imposto;

        public ImpostoAteDozeMeses()
        {
            // Definido no Construtor para fins de futuras implemenções na definição do valor no AppSettings ou XML
            // Imposto Até 06 meses: 22,5%
            _imposto = 22.5m;
        }

        /// <summary>
        ///  Cáclula o Imposto para investimentos de até 12 meses
        /// </summary>
        /// <param name="valorBruto"></param>
        /// <returns></returns>
        public decimal CalcularImposto(decimal valorBruto)
        {
            return valorBruto.CalcularValorComImposto(_imposto);
        }
    }

    public class ImpostoAteVinteQuatroMeses : ICalculadorImposto
    {
        private readonly decimal _imposto;

        public ImpostoAteVinteQuatroMeses()
        {
            // Definido no Construtor para fins de futuras implemenções na definição do valor no AppSettings ou XML
            // Imposto Até 06 meses: 22,5%
            _imposto = 22.5m;
        }

        /// <summary>
        ///  Cáclula o Imposto para investimentos de até 24 meses
        /// </summary>
        /// <param name="valorBruto"></param>
        /// <returns></returns>
        public decimal CalcularImposto(decimal valorBruto)
        {
            return valorBruto.CalcularValorComImposto(_imposto);
        }
    }

    public class ImpostoAcimaDeVinteQuatroMeses : ICalculadorImposto
    {
        private readonly decimal _imposto;

        public ImpostoAcimaDeVinteQuatroMeses()
        {
            // Definido no Construtor para fins de futuras implemenções na definição do valor no AppSettings ou XML
            // Imposto Até 06 meses: 22,5%
            _imposto = 22.5m;
        }

        /// <summary>
        ///  Cáclula o Imposto para investimentos acima de 24 meses
        /// </summary>
        /// <param name="valorBruto"></param>
        /// <returns></returns>
        public decimal CalcularImposto(decimal valorBruto)
        {
            return valorBruto.CalcularValorComImposto(_imposto);
        }
    }
}
