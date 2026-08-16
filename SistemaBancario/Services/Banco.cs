using SistemaBancario.Exceptions;
using SistemaBancario.Models;

namespace SistemaBancario.Services;

public class Banco
{
    private readonly List<Cliente> _clientes = new();

    public string Nome { get; }

    public Banco(string nome)
    {
        Nome = nome;
    }

    public void AdicionarCliente(Cliente cliente) => _clientes.Add(cliente);

    public IReadOnlyList<Cliente> Clientes => _clientes.AsReadOnly();

    public void Transferir(ContaBancaria origem, ContaBancaria destino, decimal valor)
    {
        origem.Sacar(valor);
        destino.Depositar(valor);
    }

    public void ExibirResumoGeral()
    {
        Console.WriteLine($"===== Resumo geral do banco {Nome} =====");
        foreach (var cliente in _clientes)
        {
            Console.WriteLine($"\nCliente: {cliente.Nome} (CPF: {cliente.Cpf})");
            foreach (var conta in cliente.Contas)
            {
                Console.WriteLine("  " + conta.ObterResumo());

                // Só contas que implementam ITaxavel têm taxa de manutenção.
                if (conta is ITaxavel taxavel)
                {
                    Console.WriteLine($"    Taxa de manutenção: {taxavel.CalcularTaxaManutencao():C}");
                }
            }
            Console.WriteLine($"  Patrimônio total: {cliente.PatrimonioTotal():C}");
        }
    }

    public decimal PatrimonioTotalDoBanco() => _clientes.Sum(c => c.PatrimonioTotal());
}
