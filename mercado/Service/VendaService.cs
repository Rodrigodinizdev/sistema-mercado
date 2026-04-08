using mercado.Models;
namespace mercado.Service;

public class VendaService
{
    public List<Venda> listaVendas = [];
    public void RealizarVenda(List<(Produto produto, int quantidade)> itens)
    {
        if (itens == null || itens.Count == 0)
        {
            Console.WriteLine("A venda deve ter pelo menos um item.");
            return;
        }

        foreach (var (produto, quantidade) in itens)
        {
            if (quantidade <= 0)
            {
                Console.WriteLine($"Quantidade inválida para o produto {produto.Nome}.");
                return;
            }

            if (quantidade > produto.QuantidadeEstoque)
            {
                Console.WriteLine($"Estoque insuficiente para o produto {produto.Nome}. Disponível: {produto.QuantidadeEstoque}");
                return;
            }
        }

        Venda venda = new Venda(DateTime.Now);

        foreach (var (produto, quantidade) in itens)
        {
            ItemVenda item = new ItemVenda(produto, quantidade, produto.Preco);
            produto.DebitarEstoque(quantidade);
            venda.AdicionarItem(item);
        }

        listaVendas.Add(venda);
        Console.WriteLine($"Venda #{venda.Id} realizada com sucesso! Total: {venda.ValorTotal:C}");
    }
}



