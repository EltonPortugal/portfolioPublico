using SistemaBancario.Exceptions;

namespace SistemaBancario.Models;


public abstract class ContaBancaria
{
    private static int _proximoNumero = 1000;


    public int Numero { get; }
    public string Titular { get; }
    protected decimal Saldo { get; set; }
    public List<string> Historico { get; } = new();

    protected ContaBancaria(string titular, decimal saldoInicial = 0)
    {
        if (string.IsNullOrWhiteSpace(titular))
            throw new ValorInvalidoException("O nome do titular não pode ser vazio.");
        if (saldoInicial < 0)
            throw new ValorInvalidoException("O saldo inicial não pode ser negativo.");

        Numero = _proximoNumero++;
        Titular = titular;
        Saldo = saldoInicial;
        RegistrarHistorico($"Conta {Numero} criada para {titular} com saldo inicial de {saldoInicial:C}.");
    }

    public decimal ConsultarSaldo() => Saldo;

    public virtual void Depositar(decimal valor)
    {
        if (valor <= 0)
            throw new ValorInvalidoException("O valor do depósito deve ser maior que zero.");

        Saldo += valor;
        RegistrarHistorico($"Depósito de {valor:C}. Novo saldo: {Saldo:C}.");
    }


    public abstract void Sacar(decimal valor);

    public virtual string ObterResumo()
    {
        return $"Conta {Numero} ({GetType().Name}) - Titular: {Titular} - Saldo: {Saldo:C}";
    }

    protected void RegistrarHistorico(string mensagem)
    {
        Historico.Add($"[{DateTime.Now:dd/MM/yyyy HH:mm:ss}] {mensagem}");
    }
}
