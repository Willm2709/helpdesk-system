public class Chamado
{
    public int Id { get; set;}
    public string Titulo { get; set; }
    public string Descricao { get; set; }
    public bool Encerrado { get; set; }

    public void Exibir()
    {
        Console.WriteLine($"ID: {Id}");
        Console.WriteLine($"Titulo: {Titulo}");
        Console.WriteLine($"Descricao: {Descricao}");
        Console.WriteLine($"Status: {(Encerrado ? "Encerrado" : "Aberto")}");

    }
}

