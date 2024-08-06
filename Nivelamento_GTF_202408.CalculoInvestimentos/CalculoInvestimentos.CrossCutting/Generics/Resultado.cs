namespace Investimentos.CrossCutting.Generics
{
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
}
