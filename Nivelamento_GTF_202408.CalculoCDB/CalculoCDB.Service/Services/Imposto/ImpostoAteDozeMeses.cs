using CalculoCDB.Service.Interfaces.Imposto;
using CalculoCDB.CrossCutting.Extensions;

namespace CalculoCDB.Service.Services.Imposto
{
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
}