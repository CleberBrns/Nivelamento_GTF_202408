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
            var valorCalculado = _calculoCDBService.CalcularCBD(1, 1200m);

            //Assert
            Assert.True(valorCalculado.Item1 > 0 && valorCalculado.Item2 > 0);
        }
    }

    public class CalculoCDBService : ICalculoCDBService
    {
        /// <summary>
        /// Efetua o Cáculo de um Investimento CBD
        /// </summary>
        /// <param name="prazo"></param>
        /// <param name="valorInvestir"></param>
        /// <returns>ValorFinalBruto, ValorFinalLiquido</returns>
        public Tuple<decimal, decimal> CalcularCBD(int prazo, decimal valorInvestir)
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
        Tuple<decimal, decimal> CalcularCBD(int prazo, decimal valorInvestir);
    } 
}
