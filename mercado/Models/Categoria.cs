namespace mercado.Models;

public class Categoria
{
    public Categoria(string nome, string descricao)
    {
        Id = ++ContadorId;
        Nome = nome;
        Descricao = descricao;
    }
    private static int ContadorId = 0;
    public int Id { get; }
    public string Nome { get; private set; }
    public string Descricao { get; private set; }

}
