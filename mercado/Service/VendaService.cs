using mercado.Interfaces;
using mercado.Models;
namespace mercado.Service;

public class VendaService
{
    private readonly IVendaRepository _repository;

    private readonly NotificationService _notification;

    public VendaService(NotificationService notification, IVendaRepository repository)
    {
        _notification = notification;
        _repository = repository;
    }
    public void RealizarVenda(List<(Produto produto, int quantidade)> itens)
    {
        if (itens == null || itens.Count == 0)
        {
            _notification.AdicionarErro("A venda deve ter pelo menos um item.");
            return;
        }

        foreach (var (produto, quantidade) in itens)
        {
            if (quantidade <= 0)
                _notification.AdicionarErro($"Quantidade inválida para o produto {produto.Nome}.");

            if (quantidade > produto.QuantidadeEstoque)
                _notification.AdicionarErro($"Estoque insuficiente para o produto {produto.Nome}. Disponível: {produto.QuantidadeEstoque}");

        }
            if(_notification.TemErros())
                return;

        Venda venda = new Venda(DateTime.Now);

        foreach (var (produto, quantidade) in itens)
        {
            ItemVenda item = new ItemVenda(produto, quantidade, produto.Preco);
            produto.DebitarEstoque(quantidade);
            venda.AdicionarItem(item);
        }

        _repository.Adicionar(venda);
        Console.WriteLine($"Venda #{venda.Id} realizada com sucesso! Total: {venda.ValorTotal:C}");
    }

     public void ListarVendas()
    {
        var vendas = _repository.ListarTodas();

        if (vendas.Count == 0)
        {
            _notification.AdicionarErro("Nenhuma venda realizada.");
            return;
        }

        Console.WriteLine("=== VENDAS ===");
        foreach (var venda in vendas)
        {
            Console.WriteLine($"Venda #{venda.Id} | Data: {venda.DataVenda} | Total: {venda.ValorTotal:C}");
            foreach (var item in venda.itens)
                Console.WriteLine($"  - {item.Produto.Nome} | Qtd: {item.Quantidade} | Preço unit.: {item.PrecoUnitario:C}");
        }
    }
}



