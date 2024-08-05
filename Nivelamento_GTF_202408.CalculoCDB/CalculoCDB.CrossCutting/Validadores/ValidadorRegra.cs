namespace CalculoCDB.CrossCutting.Validadores
{
    public class ValidadorRegra
    {
        private readonly List<string> _mensagensValidacao;

        public ValidadorRegra()
        {
            _mensagensValidacao = new List<string>();
        }

        public static ValidadorRegra New()
        {
            return new ValidadorRegra();
        }

        /// <summary>
        /// Valida a condição informada e adiciona uma mensagem definida a lista de Mensages de Validação
        /// Para Regras Múltiplas
        /// </summary>
        /// <param name="regraInvalida"></param>
        /// <param name="mensagemValidacao"></param>
        /// <returns></returns>
        public ValidadorRegra Quando(bool regraInvalida, string mensagemValidacao)
        {
            if (regraInvalida)
                _mensagensValidacao.Add(mensagemValidacao);

            return this;
        }

        public void ThrowMensagensValidacao()
        {
            if (_mensagensValidacao.Any())
                throw new ValidadorException(_mensagensValidacao);
        }
    }
}
