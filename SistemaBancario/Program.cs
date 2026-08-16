using SistemaBancario.Exceptions;
using SistemaBancario.Models;
using SistemaBancario.Services;


var banco = new Banco("Banco Estágio S.A.");

var joao = new Cliente("João Silva", "111.111.111-11");
var maria = new Cliente("Maria Souza", "222.222.222-22");

var contaCorrenteJoao = new ContaCorrente(joao.Nome, saldoInicial: 1000m, limiteChequeEspecial: 300m);
var contaPoupancaJoao = new ContaPoupanca(joao.Nome, saldoInicial: 5000m, taxaRendimentoMensal: 0.006m);
joao.AdicionarConta(contaCorrenteJoao);
joao.AdicionarConta(contaPoupancaJoao);

var contaCorrenteMaria = new ContaCorrente(maria.Nome, saldoInicial: 200m);
maria.AdicionarConta(contaCorrenteMaria);

banco.AdicionarCliente(joao);
banco.AdicionarCliente(maria);

Console.WriteLine("--- Operações ---");

contaCorrenteJoao.Depositar(150m);
contaPoupancaJoao.AplicarRendimento();
banco.Transferir(contaCorrenteJoao, contaCorrenteMaria, 100m);


ContaBancaria[] contas = { contaCorrenteJoao, contaPoupancaJoao, contaCorrenteMaria };

foreach (var conta in contas)
{
    try
    {
        conta.Sacar(50m);
        Console.WriteLine($"Saque de R$50 realizado na conta {conta.Numero}.");
    }
    catch (SaldoInsuficienteException ex)
    {
        Console.WriteLine($"Falha ao sacar na conta {conta.Numero}: {ex.Message}");
    }
}

Console.WriteLine("\n--- Testando validações ---");
try
{
    contaPoupancaJoao.Sacar(999999m);
}
catch (SaldoInsuficienteException ex)
{
    Console.WriteLine($"Erro esperado: {ex.Message}");
}

try
{
    contaCorrenteMaria.Depositar(-10m);
}
catch (ValorInvalidoException ex)
{
    Console.WriteLine($"Erro esperado: {ex.Message}");
}

Console.WriteLine();
banco.ExibirResumoGeral();

Console.WriteLine($"\nPatrimônio total do banco: {banco.PatrimonioTotalDoBanco():C}");

Console.WriteLine("\n--- Histórico da conta corrente do João ---");
foreach (var linha in contaCorrenteJoao.Historico)
{
    Console.WriteLine(linha);
}
