namespace CalculoCDB.CrossCutting.Resources
{
    public static class GeneralResource
    {
        public static string ValorDoParamentroNaoPodeSerZero(string nomeParametro)
        {
            return $"Valor do(a) {nomeParametro} não pode ser igual a Zero!";
        }
    }
}
