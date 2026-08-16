namespace SistemaBancario.Exceptions;

public class ValorInvalidoException : Exception
{
    public ValorInvalidoException(string mensagem) : base(mensagem)
    {
    }
}
