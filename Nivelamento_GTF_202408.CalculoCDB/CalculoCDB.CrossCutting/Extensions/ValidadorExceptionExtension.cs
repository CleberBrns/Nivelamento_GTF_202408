using CalculoCDB.CrossCutting.Validadores;

namespace CalculoCDB.CrossCutting.Extensions
{
    public static class ValidadorExceptionExtension
    {
        public static string ResumoMensagensValidacao(this ValidadorException validadorExcecao)
        {
            return !string.IsNullOrEmpty(validadorExcecao.ConcatMensagensExcecao) ?
                validadorExcecao.ConcatMensagensExcecao : validadorExcecao.Message;
        }
    }
}
