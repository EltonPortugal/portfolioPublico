using SistemaBancario.Exceptions;

namespace SistemaBancario.Models;


public class Cliente
{
    public string Nome { get; }
    public string Cpf { get; }
    private readonly List<ContaBancaria> _contas = new();

    public IReadOnlyList<ContaBancaria> Contas => _contas.AsReadOnly();

    public Cliente(string nome, string cpf)
    {
        if (string.IsNullOrWhiteSpace(nome))
            throw new ValorInvalidoException("O nome do cliente não pode ser vazio.");
        if (string.IsNullOrWhiteSpace(cpf))
            throw new ValorInvalidoException("O CPF do cliente não pode ser vazio.");

        Nome = nome;
        Cpf = cpf;
    }

    public void AdicionarConta(ContaBancaria conta) => _contas.Add(conta);

    public decimal PatrimonioTotal() => _contas.Sum(c => c.ConsultarSaldo());
}