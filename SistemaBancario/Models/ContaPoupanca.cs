using SistemaBancario.Exceptions;

namespace SistemaBancario.Models;

public class ContaPoupanca : ContaBancaria, ITaxavel
{
    public decimal TaxaRendimentoMensal { get; }

    public ContaPoupanca(string titular, decimal saldoInicial = 0, decimal taxaRendimentoMensal = 0.005m)
        : base(titular, saldoInicial)
    {
        TaxaRendimentoMensal = taxaRendimentoMensal;
    }

    public override void Sacar(decimal valor)
    {
        if (valor <= 0)
            throw new ValorInvalidoException("O valor do saque deve ser maior que zero.");

        if (valor > Saldo)
            throw new SaldoInsuficienteException(Saldo, valor);

        Saldo -= valor;
        RegistrarHistorico($"Saque de {valor:C}. Novo saldo: {Saldo:C}.");
    }

    public decimal CalcularTaxaManutencao() => 0m;

    public void AplicarRendimento()
    {
        var rendimento = Saldo * TaxaRendimentoMensal;
        Saldo += rendimento;
        RegistrarHistorico($"Rendimento mensal aplicado: {rendimento:C}. Novo saldo: {Saldo:C}.");
    }

    public override string ObterResumo()
    {
        return base.ObterResumo() + $" - Rendimento mensal: {TaxaRendimentoMensal:P2}";
    }
}
