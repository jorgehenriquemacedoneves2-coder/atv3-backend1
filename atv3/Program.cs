using System;
using System.Collections.Generic;
using System.IO;

class Cliente
{
    public int Id;
    public string Nome;
    public string Telefone;

    public Cliente(int id, string nome, string telefone)
    {
        Id = id;
        Nome = nome;
        Telefone = telefone;
    }
}

class Aparelho
{
    public string Modelo;
    public string Problema;

    public Aparelho(string modelo, string problema)
    {
        Modelo = modelo;
        Problema = problema;
    }
}

class OrdemServico
{
    public int Numero;
    public Cliente Cliente;
    public Aparelho Aparelho;
    public double Valor;
    public string Status;

    public OrdemServico(int numero, Cliente cliente,
        Aparelho aparelho, double valor)
    {
        Numero = numero;
        Cliente = cliente;
        Aparelho = aparelho;
        Valor = valor;
        Status = "Em andamento";
    }
}

class Program
{
    static List<Cliente> clientes = new List<Cliente>();
    static List<OrdemServico> ordens = new List<OrdemServico>();

    static int proximoId = 1;
    static int proximaOrdem = 1;

    static void Main(string[] args)
    {
        int opcao = -1;

        while (opcao != 0)
        {
            Console.Clear();

            Console.WriteLine("================================");
            Console.WriteLine("         TECHCONTROL");
            Console.WriteLine(" Sistema de Assistencia Tecnica");
            Console.WriteLine("================================");
            Console.WriteLine("1 - Cadastrar cliente");
            Console.WriteLine("2 - Listar clientes");
            Console.WriteLine("3 - Criar ordem de servico");
            Console.WriteLine("4 - Listar ordens de servico");
            Console.WriteLine("5 - Alterar status");
            Console.WriteLine("6 - Salvar relatorio");
            Console.WriteLine("0 - Sair");
            Console.Write("Escolha: ");

            if (!int.TryParse(Console.ReadLine(), out opcao))
            {
                opcao = -1;
            }

            switch (opcao)
            {
                case 1:
                    CadastrarCliente();
                    break;

                case 2:
                    ListarClientes();
                    break;

                case 3:
                    CriarOrdem();
                    break;

                case 4:
                    ListarOrdens();
                    break;

                case 5:
                    AlterarStatus();
                    break;

                case 6:
                    SalvarRelatorio();
                    break;

                case 0:
                    Console.WriteLine("Programa encerrado.");
                    break;

                default:
                    Console.WriteLine("Opcao invalida.");
                    Pausar();
                    break;
            }
        }
    }

    static void CadastrarCliente()
    {
        Console.Clear();
        Console.WriteLine("=== CADASTRO DE CLIENTE ===");

        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Telefone: ");
        string telefone = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(nome) ||
            string.IsNullOrWhiteSpace(telefone))
        {
            Console.WriteLine("Preencha todos os campos.");
            Pausar();
            return;
        }

        Cliente cliente = new Cliente(
            proximoId, nome, telefone);

        clientes.Add(cliente);
        proximoId++;

        Console.WriteLine("Cliente cadastrado com sucesso!");
        Pausar();
    }

    static void ListarClientes()
    {
        Console.Clear();
        Console.WriteLine("=== LISTA DE CLIENTES ===");

        if (clientes.Count == 0)
        {
            Console.WriteLine("Nenhum cliente cadastrado.");
        }
        else
        {
            foreach (Cliente cliente in clientes)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("ID: " + cliente.Id);
                Console.WriteLine("Nome: " + cliente.Nome);
                Console.WriteLine("Telefone: " + cliente.Telefone);
            }
        }

        Pausar();
    }

    static void CriarOrdem()
    {
        Console.Clear();
        Console.WriteLine("=== NOVA ORDEM DE SERVICO ===");

        if (clientes.Count == 0)
        {
            Console.WriteLine("Cadastre um cliente primeiro.");
            Pausar();
            return;
        }

        foreach (Cliente cliente in clientes)
        {
            Console.WriteLine(
                "ID: " + cliente.Id +
                " | Nome: " + cliente.Nome);
        }

        Console.Write("Digite o ID do cliente: ");
        int id;

        if (!int.TryParse(Console.ReadLine(), out id))
        {
            Console.WriteLine("ID invalido.");
            Pausar();
            return;
        }

        Cliente clienteEscolhido = null;

        foreach (Cliente cliente in clientes)
        {
            if (cliente.Id == id)
            {
                clienteEscolhido = cliente;
                break;
            }
        }

        if (clienteEscolhido == null)
        {
            Console.WriteLine("Cliente nao encontrado.");
            Pausar();
            return;
        }

        Console.Write("Modelo do aparelho: ");
        string modelo = Console.ReadLine();

        Console.Write("Problema do aparelho: ");
        string problema = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(modelo) ||
            string.IsNullOrWhiteSpace(problema))
        {
            Console.WriteLine("Preencha os dados do aparelho.");
            Pausar();
            return;
        }

        Console.Write("Valor do servico: ");

        double valor;

        if (!double.TryParse(Console.ReadLine(), out valor) ||
            valor < 0)
        {
            Console.WriteLine("Valor invalido.");
            Pausar();
            return;
        }

        Aparelho aparelho = new Aparelho(modelo, problema);

        OrdemServico ordem = new OrdemServico(
            proximaOrdem, clienteEscolhido, aparelho, valor);

        ordens.Add(ordem);
        proximaOrdem++;

        Console.WriteLine("Ordem de servico criada!");
        Pausar();
    }

    static void ListarOrdens()
    {
        Console.Clear();
        Console.WriteLine("=== ORDENS DE SERVICO ===");

        if (ordens.Count == 0)
        {
            Console.WriteLine("Nenhuma ordem cadastrada.");
        }
        else
        {
            foreach (OrdemServico ordem in ordens)
            {
                Console.WriteLine("------------------------");
                Console.WriteLine("Numero: " + ordem.Numero);
                Console.WriteLine("Cliente: " + ordem.Cliente.Nome);
                Console.WriteLine("Telefone: " + ordem.Cliente.Telefone);
                Console.WriteLine("Aparelho: " + ordem.Aparelho.Modelo);
                Console.WriteLine("Problema: " + ordem.Aparelho.Problema);
                Console.WriteLine("Valor: R$ " + ordem.Valor.ToString("F2"));
                Console.WriteLine("Status: " + ordem.Status);
            }
        }

        Pausar();
    }

    static void AlterarStatus()
    {
        Console.Clear();
        Console.WriteLine("=== ALTERAR STATUS ===");

        if (ordens.Count == 0)
        {
            Console.WriteLine("Nenhuma ordem cadastrada.");
            Pausar();
            return;
        }

        foreach (OrdemServico ordem in ordens)
        {
            Console.WriteLine(
                "Numero: " + ordem.Numero +
                " | Cliente: " + ordem.Cliente.Nome +
                " | Status: " + ordem.Status);
        }

        Console.Write("Numero da ordem: ");
        int numero;

        if (!int.TryParse(Console.ReadLine(), out numero))
        {
            Console.WriteLine("Numero invalido.");
            Pausar();
            return;
        }

        OrdemServico ordemEscolhida = null;

        foreach (OrdemServico ordem in ordens)
        {
            if (ordem.Numero == numero)
            {
                ordemEscolhida = ordem;
                break;
            }
        }

        if (ordemEscolhida == null)
        {
            Console.WriteLine("Ordem nao encontrada.");
            Pausar();
            return;
        }

        Console.WriteLine("1 - Em andamento");
        Console.WriteLine("2 - Concluido");
        Console.WriteLine("3 - Aguardando peca");
        Console.Write("Escolha o novo status: ");

        string escolha = Console.ReadLine();

        if (escolha == "1")
        {
            ordemEscolhida.Status = "Em andamento";
        }
        else if (escolha == "2")
        {
            ordemEscolhida.Status = "Concluido";
        }
        else if (escolha == "3")
        {
            ordemEscolhida.Status = "Aguardando peca";
        }
        else
        {
            Console.WriteLine("Opcao invalida.");
            Pausar();
            return;
        }

        Console.WriteLine("Status alterado com sucesso!");
        Pausar();
    }

    static void SalvarRelatorio()
    {
        try
        {
            StreamWriter arquivo =
                new StreamWriter("relatorio_techcontrol.txt");

            arquivo.WriteLine("RELATORIO TECHCONTROL");
            arquivo.WriteLine("=====================");

            arquivo.WriteLine("");
            arquivo.WriteLine("CLIENTES");

            foreach (Cliente cliente in clientes)
            {
                arquivo.WriteLine(
                    "ID: " + cliente.Id +
                    " | Nome: " + cliente.Nome +
                    " | Telefone: " + cliente.Telefone);
            }

            arquivo.WriteLine("");
            arquivo.WriteLine("ORDENS DE SERVICO");

            foreach (OrdemServico ordem in ordens)
            {
                arquivo.WriteLine("---------------------");
                arquivo.WriteLine("Numero: " + ordem.Numero);
                arquivo.WriteLine("Cliente: " + ordem.Cliente.Nome);
                arquivo.WriteLine("Aparelho: " + ordem.Aparelho.Modelo);
                arquivo.WriteLine("Problema: " + ordem.Aparelho.Problema);
                arquivo.WriteLine("Valor: R$ " + ordem.Valor.ToString("F2"));
                arquivo.WriteLine("Status: " + ordem.Status);
            }

            arquivo.Close();

            Console.WriteLine(
                "Relatorio salvo em relatorio_techcontrol.txt");
        }
        catch (Exception erro)
        {
            Console.WriteLine("Erro ao salvar relatorio: " + erro.Message);
        }

        Pausar();
    }

    static void Pausar()
    {
        Console.WriteLine("");
        Console.WriteLine("Pressione ENTER para continuar...");
        Console.ReadLine();
    }
}
