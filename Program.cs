using System;
using System.Collections.Generic;
using helpdesk_system.Models;

class Program
{
    static List<Usuario> usuarios = new List<Usuario>();
    static Usuario usuarioLogado = null;

    static List<Chamado> chamados = new List<Chamado>();
    static int proximoId = 1;

    static void Main()
    {
        bool executando = true;

        while (executando)
        {
            Console.WriteLine("=== MENU PRINCIPAL ===");
            Console.WriteLine("1 – Cadastrar Usuário");
            Console.WriteLine("2 – Login");
            Console.WriteLine("3 – Sistema de Chamados");
            Console.WriteLine("4 – Sair");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    CadastrarUsuario();
                    break;
                case "2":
                    Login();
                    break;
                case "3":
                    if (usuarioLogado != null)
                    {
                        MenuChamados();
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Você precisa estar logado para acessar o sistema de chamados.\n");
                    }
                    break;
                case "4":
                    executando = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void CadastrarUsuario()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Senha: ");
        string senha = Console.ReadLine();

        Usuario novoUsuario = new Usuario
        {
            Nome = nome,
            Email = email,
            Senha = senha
        };

        usuarios.Add(novoUsuario);
        Console.WriteLine("✅ Usuário cadastrado com sucesso!\n");

        if (usuarios.Exists(u => u.Email == email))
        {
            Console.WriteLine("❌ Já existe um usuário com esse email.\n");
            return;
        }   
    }

    static void Login()
    {
        Console.Write("Email: ");
        string email = Console.ReadLine();

        Console.Write("Senha: ");
        string senha = Console.ReadLine();

        Usuario usuario = usuarios.Find(u => u.Email == email && u.Senha == senha);

        if (usuario != null)
        {
            usuarioLogado = usuario;
            Console.WriteLine($"\n✅ Login realizado com sucesso! Bem-vindo(a), {usuarioLogado.Nome}.\n");
        }
        else
        {
            Console.WriteLine("\n❌ Email ou senha inválidos.\n");
        }
    }

    static void MenuChamados()
    {
        bool executando = true;

        while (executando)
        {
            Console.WriteLine("====== SISTEMA DE CHAMADOS ======");
            Console.WriteLine($"Usuário logado: {usuarioLogado.Nome}\n");
            Console.WriteLine("1 - Abrir Chamado");
            Console.WriteLine("2 - Listar Chamados");
            Console.WriteLine("3 - Encerrar Chamado");
            Console.WriteLine("4 - Voltar ao menu principal");
            Console.Write("Escolha uma opção: ");
            string opcao = Console.ReadLine();

            switch (opcao)
            {
                case "1":
                    AbrirChamado();
                    break;
                case "2":
                    ListarChamados();
                    break;
                case "3":
                    EncerrarChamado();
                    break;
                case "4":
                    executando = false;
                    break;
                default:
                    Console.WriteLine("Opção inválida.");
                    break;
                case "5":
                    usuarioLogado = null;
                    Console.WriteLine();
                    executando = false;
                    break;
            }
        }
    }

    static void AbrirChamado()
    {
        Console.Write("Digite o título do chamado: ");
        string titulo = Console.ReadLine();
        Console.Write("Digite a descrição: ");
        string descricao = Console.ReadLine();

        Chamado novoChamado = new Chamado
        {
            Id = proximoId++,
            Titulo = titulo,
            Descricao = descricao,
            Encerrado = false
        };

        chamados.Add(novoChamado);
        Console.WriteLine("✅ Chamado aberto com sucesso!\n");
    }

    static void ListarChamados()
    {
        if (chamados.Count == 0)
        {
            Console.WriteLine("Nenhum chamado encontrado.");
            return;
        }

        foreach (var chamado in chamados)
        {
            chamado.Exibir();
        }
    }

    static void EncerrarChamado()
    {
        Console.Write("Digite o ID do chamado a encerrar: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Chamado chamado = chamados.Find(c => c.Id == id);
            if (chamado != null)
            {
                chamado.Encerrado = true;
                Console.WriteLine("✅ Chamado encerrado.\n");
            }
            else
            {
                Console.WriteLine("Chamado não encontrado.");
            }
        }
        else
        {
            Console.WriteLine("ID inválido.");
        }
    }

}
