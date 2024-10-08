using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        Console.Write("Digite seu nome: ");
        string nomeTitular = Console.ReadLine();

        Conta conta = new Conta(nomeTitular, "Corrente");
        bool sair = false;

        while (!sair)
        {
            Console.Clear();
            Console.WriteLine("Bem-vindo ao Caixa Eletrônico!");
            Console.WriteLine("1. Saque");
            Console.WriteLine("2. Depósito");
            Console.WriteLine("3. Extrato");
            Console.WriteLine("4. Transferência");
            Console.WriteLine("5. Sair");
            Console.Write("Escolha uma opção: ");
            int opcao = int.Parse(Console.ReadLine());

            switch (opcao)
            {
                case 1:
                    Console.Write("Informe o valor do saque: ");
                    double valorSaque = double.Parse(Console.ReadLine());
                    conta.Sacar(valorSaque);
                    break;
                case 2:
                    Console.Write("Informe o valor do depósito: ");
                    double valorDeposito = double.Parse(Console.ReadLine());
                    conta.Depositar(valorDeposito);
                    break;
                case 3:
                    conta.ExibirExtrato();
                    conta.SalvarExtratoEmArquivo();
                    break;
                case 4:
                    Console.Write("Informe o valor da transferência: ");
                    double valorTransferencia = double.Parse(Console.ReadLine());
                    // Para transferências, você pode solicitar o nome da conta destino se quiser
                    // Para simplificar, faremos a transferência para uma conta fixa
                    Conta contaDestino = new Conta("Maria", "Poupança"); // conta fixa para a transferência
                    conta.Transferir(contaDestino, valorTransferencia);
                    break;
                case 5:
                    sair = true;
                    Console.WriteLine("Saindo...");
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }

            if (!sair)
            {
                Console.WriteLine("Pressione qualquer tecla para continuar...");
                Console.ReadKey();
            }
        }
    }
}

class Conta
{
    private string titular;
    private string tipoConta;
    private double saldo;
    private List<string> extrato;

    public Conta(string titular, string tipoConta)
    {
        this.titular = titular;
        this.tipoConta = tipoConta;
        this.saldo = 0;
        this.extrato = new List<string>();
    }

    public void Sacar(double valor)
    {
        if (valor > saldo)
        {
            Console.WriteLine("Saldo insuficiente.");
        }
        else if (valor <= 0)
        {
            Console.WriteLine("Valor inválido.");
        }
        else
        {
            saldo -= valor;
            extrato.Add($"Saque: {valor:C} - Saldo atual: {saldo:C}");
            Console.WriteLine($"Saque de {valor:C} realizado com sucesso.");
        }
    }

    public void Depositar(double valor)
    {
        if (valor <= 0)
        {
            Console.WriteLine("Valor inválido.");
        }
        else
        {
            saldo += valor;
            extrato.Add($"Depósito: {valor:C} - Saldo atual: {saldo:C}");
            Console.WriteLine($"Depósito de {valor:C} realizado com sucesso.");
        }
    }

    public void Transferir(Conta contaDestino, double valor)
    {
        if (valor > saldo)
        {
            Console.WriteLine("Saldo insuficiente para transferência.");
        }
        else if (valor <= 0)
        {
            Console.WriteLine("Valor inválido.");
        }
        else
        {
            saldo -= valor;
            contaDestino.Depositar(valor);
            extrato.Add($"Transferência: {valor:C} para {contaDestino.titular} - Saldo atual: {saldo:C}");
            Console.WriteLine($"Transferência de {valor:C} para {contaDestino.titular} realizada com sucesso.");
        }
    }

    public void ExibirExtrato()
    {
        Console.WriteLine("\nExtrato:");
        foreach (var movimento in extrato)
        {
            Console.WriteLine(movimento);
        }
        Console.WriteLine($"Saldo final: {saldo:C}");
    }

    public void SalvarExtratoEmArquivo()
    {
        string filePath = $"{titular}_extrato.txt";
        File.WriteAllLines(filePath, extrato);
        Console.WriteLine($"Extrato salvo em '{filePath}'.");
    }
}
