using SistemaBancario.Exceptions;

namespace SistemaBancario.Models;

public class ContaCorrente : ContaBancaria, ITaxavel
{
    public decimal LimiteChequeEspecial { get; }

    public ContaCorrente(string titular, decimal saldoInicial = 0, decimal limiteChequeEspecial = 500m)
        : base(titular, saldoInicial)
    {
        if (limiteChequeEspecial < 0)
            throw new ValorInvalidoException("O limite do cheque especial não pode ser negativo.");

        LimiteChequeEspecial = limiteChequeEspecial;
    }

    public override void Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ValorInvalidoException("O valor do saque deve ser maior que zero.");

        if (Saldo - valor < -LimiteChequeEspecial)
            throw new SaldoInsuficienteException(Saldo + LimiteChequeEspecial, valor);

        Saldo -= valor;
        RegistrarHistorico($"Saque de {valor:C}. Novo saldo: {Saldo:C}.");
    }

    public decimal CalcularTaxaManutencao() => 12.90m;

    public override string ObterResumo()
    {
        return base.ObterResumo() + $" - Limite cheque especial: {LimiteChequeEspecial:C}";
    }
}
