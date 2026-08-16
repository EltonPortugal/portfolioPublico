namespace SistemaBancario.Exceptions;

public class SaldoInsuficienteException : Exception
{
    public decimal SaldoAtual { get; }
    public decimal ValorSolicitado { get; }

    public SaldoInsuficienteException(decimal saldoAtual, decimal valorSolicitado)
        : base($"Saldo insuficiente. Saldo atual: {saldoAtual:C}, valor solicitado: {valorSolicitado:C}.")
    {
        SaldoAtual = saldoAtual;
        ValorSolicitado = valorSolicitado;
    }
}
