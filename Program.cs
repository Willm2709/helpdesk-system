using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using helpdesk_system.Models;

class Program
{
    static List<Usuario> usuarios = new List<Usuario>();
    static Usuario usuarioLogado = null;

    static List<Chamado> chamados = new List<Chamado>();
    static int proximoId = 1;

    static void Main()
    {
        CarregarUsuarios();
        CarregarChamados();

        bool executando = true;

        while (executando)
        {
            Console.WriteLine("\n=== MENU PRINCIPAL ===");
            Console.WriteLine("1 - Cadastrar Usuário");
            Console.WriteLine("2 - Login");
            Console.WriteLine("3 - Sistema de Chamados");
            Console.WriteLine("4 - Modo Suporte");
            Console.WriteLine("5 - Sair");
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
                        Console.WriteLine("⚠️ Você precisa estar logado para acessar o sistema de chamados.");
                    }
                    break;

                case "4":
                    if (usuarioLogado != null && usuarioLogado.IsSuporte)
                    {
                        ModoSuporte();
                    }
                    else
                    {
                        Console.WriteLine("⚠️ Acesso negado. Apenas usuários de suporte podem acessar.");
                    }
                    break;

                case "5":
                    executando = false;
                    break;


                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    // ================= USUÁRIOS =================

    static void CadastrarUsuario()
    {
        Console.Write("Nome: ");
        string nome = Console.ReadLine();

        Console.Write("Email: ");
        string email = Console.ReadLine();

        if (usuarios.Exists(u => u.Email == email))
        {
            Console.WriteLine("❌ Já existe um usuário com esse email.");
            return;
        }

        Console.Write("Senha: ");
        string senha = Console.ReadLine();

        Console.Write("Este usuário é suporte? (s/n): ");
        string isSuporteInput = Console.ReadLine().ToLower();
        bool isSuporte = isSuporteInput == "s";

        Usuario novoUsuario = new Usuario
        {
            Nome = nome,
            Email = email,
            Senha = senha,
            IsSuporte = isSuporte
        };

        usuarios.Add(novoUsuario);
        SalvarUsuarios();

        Console.WriteLine("✅ Usuário cadastrado com sucesso!");
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
            Console.WriteLine($"✅ Login realizado! Bem-vindo(a), {usuarioLogado.Nome}.");
        }
        else
        {
            Console.WriteLine("❌ Email ou senha inválidos.");
        }
    }

    // ================= CHAMADOS =================

    static void MenuChamados()
    {
        bool executando = true;

        while (executando)
        {
            Console.WriteLine("\n====== SISTEMA DE CHAMADOS ======");
            Console.WriteLine($"Usuário logado: {usuarioLogado.Nome}");
            Console.WriteLine("1 - Abrir Chamado");
            Console.WriteLine("2 - Listar Chamados");
            Console.WriteLine("3 - Encerrar Chamado");
            Console.WriteLine("4 - Voltar ao menu principal");
            Console.WriteLine("5 - Logout");
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

                case "5":
                    usuarioLogado = null;
                    executando = false;
                    Console.WriteLine("🔒 Logout realizado com sucesso.");
                    break;

                default:
                    Console.WriteLine("Opção inválida.");
                    break;
            }
        }
    }

    static void AbrirChamado()
    {
        Console.Write("Título do chamado: ");
        string titulo = Console.ReadLine();

        Console.Write("Descrição: ");
        string descricao = Console.ReadLine();

        Chamado novoChamado = new Chamado
        {
            Id = proximoId++,
            Titulo = titulo,
            Descricao = descricao,
            Encerrado = false,
            EmailUsuario = usuarioLogado.Email
        };

        chamados.Add(novoChamado);
        SalvarChamados();

        Console.WriteLine("✅ Chamado aberto com sucesso!");
    }

    static void ListarChamados()
    {
        var chamadosDoUsuario = chamados.FindAll(c => c.EmailUsuario == usuarioLogado.Email);

        if (chamadosDoUsuario.Count == 0)
        {
            Console.WriteLine("Você não possui chamados.");
            return;
        }

        foreach (var chamado in chamadosDoUsuario)
        {
            chamado.Exibir();
        }
    }

    static void EncerrarChamado()
    {
        Console.Write("Digite o ID do chamado: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            Chamado chamado = chamados.Find(c => c.Id == id);

            if (chamado != null)
            {
                chamado.Encerrado = true;
                SalvarChamados();
                Console.WriteLine("✅ Chamado encerrado.");
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

    static void SalvarUsuarios()
    {
        string json = JsonSerializer.Serialize(usuarios);
        File.WriteAllText("usuarios.json", json);
    }

    static void CarregarUsuarios()
    {
        if (File.Exists("usuarios.json"))
        {
            string json = File.ReadAllText("usuarios.json");
            usuarios = JsonSerializer.Deserialize<List<Usuario>>(json) ?? new List<Usuario>();
        }
    }

    static void SalvarChamados()
    {
        string json = JsonSerializer.Serialize(chamados);
        File.WriteAllText("chamados.json", json);
    }

    static void CarregarChamados()
    {
        if (File.Exists("chamados.json"))
        {
            string json = File.ReadAllText("chamados.json");
            chamados = JsonSerializer.Deserialize<List<Chamado>>(json) ?? new List<Chamado>();
        }
    }

    static void ModoSuporte()
    {
        Console.WriteLine("\n=== MODO SUPORTE ===");
        Console.WriteLine("Listando todos os chamados abertos:\n");

        if (chamados.Count == 0)
        {
            Console.WriteLine("Nenhum chamado no sistema.");
            return;
        }

        foreach (var chamado in chamados)
        {
            chamado.Exibir();
        }
    }

}
