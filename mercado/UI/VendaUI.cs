namespace mercado.UI;

using mercado.Service;
using mercado.DTOs;
using mercado.Models;

public class VendaUI
{
    private readonly VendaService _vendaService;
    private readonly NotificationService _notification;
    private readonly ProdutoService _produtoService;

    public VendaUI(VendaService vendaService, ProdutoService produtoService, NotificationService notification)
    {
        _vendaService = vendaService;
        _produtoService = produtoService;
        _notification = notification;
    }

    public void Realizar()
    {
        Console.Clear();

        if (_produtoService.ListarTodos().Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        List<(Produto produto, int quantidade)> itens = [];

        while (true)
        {
            _produtoService.ListarProdutos();

            Console.Write("\nID do produto (0 para finalizar): ");
            int idProduto;
            while (!int.TryParse(Console.ReadLine(), out idProduto) || idProduto < 0)
            {
                Console.WriteLine("ERRO! Digite um ID válido.");
                Console.Write("\nID do produto (0 para finalizar): ");
            }

            if (idProduto == 0) break;

            var produto = _produtoService.BuscarPorId(idProduto);
            if (produto == null)
            {
                Console.WriteLine("Produto não encontrado!");
                continue;
            }

            int quantidade;
            Console.Write("Quantidade: ");
            while (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade <= 0)
            {
                Console.WriteLine("ERRO! Digite uma quantidade válida.");
                Console.Write("Quantidade: ");
            }

            itens.Add((produto, quantidade));
            Console.WriteLine($"{produto.Nome} adicionado à venda.");
        }

        if (itens.Count == 0)
        {
            Console.WriteLine("Nenhum item adicionado. Venda cancelada.");
            return;
        }

        var dto = new CriarVendaDto
        {
            Itens = itens
        };

        _vendaService.RealizarVenda(dto);
    }


    public void Listar()
    {
        Console.Clear();
        _vendaService.ListarVendas();
        if (_notification.HasErros()) _notification.ExibirErros();
    }
}
