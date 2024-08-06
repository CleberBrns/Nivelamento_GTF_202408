using Investimentos.CrossCutting.Generics;
using Investimentos.Domain.Models.CalculoCDB;
using Investimentos.Service.Interfaces.CalculoCdb;
using Microsoft.AspNetCore.Mvc;

namespace CalculoInvestimentos.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculoInvestimentosController : ControllerBase
    {
        private readonly ICalculoCdbService _calculoCdbService;

        public CalculoInvestimentosController(ICalculoCdbService calculoCdbService)
        {
            _calculoCdbService = calculoCdbService;
        }

        /// <summary>
        /// Executa o Cáculo de Investimento CDB, retornando os valores, Bruto e Liquído finais
        /// </summary>
        /// <param name="prazo">Prazo do Investimento - Em Meses</param>
        /// <param name="valor">Valor do Investimento</param>
        /// <returns></returns>
        [Route("CalcularCDB")]
        [HttpGet]
        [ProducesResponseType(typeof(Resultado<InvestimentoCalculado>), 200)]
        public async Task<IActionResult> CalcularInvestimentoCdbAsync(int prazo, decimal valor)
        {
            try
            {
                var resultado = await Task.FromResult<Resultado<InvestimentoCalculado>>(
                    _calculoCdbService.CalcularInvestimentoCdb(prazo, valor));

                if (resultado.Sucesso)
                {
                    return Ok(resultado);
                }

                return Conflict(resultado);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
