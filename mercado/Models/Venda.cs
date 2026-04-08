namespace mercado.Models;

public class Venda
{
    public Venda(DateTime dataVenda)
    {
        Id = ++ContadorId;
        DataVenda = dataVenda;
        ValorTotal = 0;
    }
    private static int ContadorId = 0;
    public int Id { get; }
    public DateTime DataVenda { get; private set; }
    public decimal ValorTotal { get; private set; }

    public List<ItemVenda> itens = [];

    public void AdicionarItem(ItemVenda item)
    {
        itens.Add(item);
        ValorTotal += item.Quantidade * item.PrecoUnitario;
    }
}
