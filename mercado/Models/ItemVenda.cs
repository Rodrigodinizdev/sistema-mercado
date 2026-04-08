namespace mercado.Models;

public class ItemVenda
{
    public ItemVenda(Produto produto, int quantidade, decimal precounitario)
    {
        Id = ++ContadorId;
        Produto = produto;
        Quantidade = quantidade;
        PrecoUnitario = precounitario;
    }
    private static int ContadorId = 0;
    public int Id { get; }
    public Produto Produto { get; private set; }
    public int Quantidade { get; private set; }
    public decimal PrecoUnitario { get; private set; }
}
