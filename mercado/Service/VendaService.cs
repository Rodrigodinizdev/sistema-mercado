using mercado.DTOs;
using mercado.Models;
using mercado.Repositories;
namespace mercado.Service;

public class VendaService
{
    private readonly IVendaRepository _repository;

    private readonly NotificationService _notification;

    public VendaService(IVendaRepository repository, NotificationService notification)
    {
        _repository = repository;
        _notification = notification;
    }
    public void RealizarVenda(CriarVendaDto dto)
    {
        if (dto.Itens == null || dto.Itens.Count == 0)
        {
            _notification.AdicionarErro("A venda deve ter pelo menos um item.");
            return;
        }

        foreach (var (produto, quantidade) in dto.Itens)
        {
            if (quantidade <= 0)
                _notification.AdicionarErro($"Quantidade inválida para o produto {produto.Nome}.");

            if (quantidade > produto.QuantidadeEstoque)
                _notification.AdicionarErro($"Estoque insuficiente para o produto {produto.Nome}. Disponível: {produto.QuantidadeEstoque}");

        }
            if(_notification.TemErros())
                return;

        Venda venda = new Venda(DateTime.Now);

        foreach (var (produto, quantidade) in dto.Itens)
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



