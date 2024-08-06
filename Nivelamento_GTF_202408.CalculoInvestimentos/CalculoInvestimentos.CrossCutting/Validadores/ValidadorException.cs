namespace Investimentos.CrossCutting.Validadores
{
    public class ValidadorException : ArgumentException
    {
        public List<string> MensagensExcecao { get; set; }

        public string ConcatMensagensExcecao { get; set; }

        public ValidadorException(string error) : base(error) { }

        public ValidadorException(List<string> mensagensExcecao)
        {
            MensagensExcecao = mensagensExcecao;
            ConcatMensagensExcecao = MensagensExcecao.Any() ? string.Join(", \n", MensagensExcecao) : string.Empty;
        }

        /// <summary>
        /// Valida a condição informada retorna a Exceção de Validação
        /// Para Regra Única
        /// </summary>
        /// <param name="regraInvalida"></param>
        /// <param name="mensagemValidacao"></param>
        public static void Validar(bool regraInvalida, string mensagemValidacao)
        {
            if (regraInvalida)
                throw new ValidadorException(mensagemValidacao);
        }
    }
}
