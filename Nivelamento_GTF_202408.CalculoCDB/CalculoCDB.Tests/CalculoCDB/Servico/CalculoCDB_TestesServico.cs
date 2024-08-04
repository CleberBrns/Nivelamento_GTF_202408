using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CalculoCDB.Tests.CalculoCDB.Servico
{
    public class CalculoCDB_TestesServico
    {
        private readonly ICalculoCDBService _calculoCDBService;

        public CalculoCDB_TestesServico()
        {
            _calculoCDBService = new CalculoCDBService();
        }

        [Fact(DisplayName = "Deve Efetuar um Calculo do CBD com sucesso")]
        public void DeveEfetuarCalculoCdb_ComSucesso()
        {
            //Arrange

            //Act
            var resultadoCalculadoInvestimento = _calculoCDBService.CalcularCBD(1, 1200m);

            //Assert
            Assert.True(
                resultadoCalculadoInvestimento.Sucesso &&
                resultadoCalculadoInvestimento.Value != null &&
                resultadoCalculadoInvestimento.Value.ValorFinalBruto > 0 &&
                resultadoCalculadoInvestimento.Value.ValorFinalLiquido > 0);
        }
    }

    public class CalculoCDBService : ICalculoCDBService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="prazo"></param>
        /// <param name="valorInvestir"></param>
        /// <returns>Retorna o Resultado do Cálculo de um investimento, contendo o Valor Final Bruto e o Valor Final Líquido</returns>
        /// <exception cref="NotImplementedException"></exception>
        Resultado<InvestimentoCalculado> ICalculoCDBService.CalcularCBD(int prazo, decimal valorInvestir)
        {
            throw new NotImplementedException();
        }
    }

    public interface ICalculoCDBService
    {
        /// <summary>
        /// Efetua o Cáculo de um Investimento CBD
        /// </summary>
        /// <param name="prazo"></param>
        /// <param name="valorInvestir"></param>
        /// <returns>ValorFinalBruto, ValorFinalLiquido</returns>
        Resultado<InvestimentoCalculado> CalcularCBD(int prazo, decimal valorInvestir);
    }

    public class Resultado<T>
    {
        public T Value { get; set; }

        public bool Sucesso { get; set; }

        public string Mensagem { get; private set; }

        public Resultado(bool sucesso, string mensagem)
        {
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
