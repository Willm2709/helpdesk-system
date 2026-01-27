namespace helpdesk_system.Models
{
    public class Chamado
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public bool Encerrado { get; set; }

        public string EmailUsuario { get; set; } // ← NOVO

        public void Exibir()
        {
            Console.WriteLine($"\nID: {Id}");
            Console.WriteLine($"Título: {Titulo}");
            Console.WriteLine($"Descrição: {Descricao}");
            Console.WriteLine($"Encerrado: {(Encerrado ? "Sim" : "Não")}");
            Console.WriteLine($"Criado por: {EmailUsuario}");
        }
    }
}
