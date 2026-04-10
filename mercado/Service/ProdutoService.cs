using mercado.Dtos;
using mercado.Models;
using mercado.Repositories;
namespace mercado.Service;

public class ProdutoService
{
    private IProdutoRepository _repository;

    private readonly NotificationService _notification;

    public ProdutoService (IProdutoRepository repository, NotificationService notification)
    {
        _repository = repository;
        _notification = notification;
    } 

    public void CadastrarProduto(CriarProdutoDto dto, Categoria categoria)
    {

        if (string.IsNullOrWhiteSpace(dto.Nome))
            _notification.AdicionarErro("Nome é obrigatório");
            
        if (dto.Preco <= 0)
            _notification.AdicionarErro("Preço deve ser maior que zero.");
        
        if (dto.QuantidadeEstoque < 0)
            _notification.AdicionarErro("Estoque não pode ser negativo");

        if (_repository.BuscarPorNome(dto.Nome) != null)
            _notification.AdicionarErro("Já existe um produto com esse nome");

        if (_notification.TemErros())
        {
            _notification.ExibirErros();
            return;
        }

        Produto produto = new Produto(dto.Nome, dto.Preco, categoria, dto.QuantidadeEstoque, dto.DataValidade);
        _repository.Adicionar(produto);
        Console.WriteLine($"Produto Cadastrado com sucesso: {produto}");
    }

    public void ListarProdutos()
    {
        var produtos = _repository.ListarTodos();

        if (produtos.Count == 0)
        {
            _notification.AdicionarErro("Nenhum produto cadastrado.");
            return;
        }

        Console.WriteLine("=== PRODUTOS ===");
        foreach (var produto in produtos)
            Console.WriteLine(produto);
        
    }

    public void RemoverProduto(int id)
    {
        var produto = _repository.BuscarPorId(id);

        if (produto == null)
        {
            _notification.AdicionarErro("Produto não encontrado.");
            return;
        }

        _repository.Remover(produto);
        Console.WriteLine($"Produto '{produto.Nome}' removido com sucesso.");
    }

    public Produto BuscarPorId(int id) => _repository.BuscarPorId(id);
    public List<Produto> ListarTodos() => _repository.ListarTodos();

}
