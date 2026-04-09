using mercado.Models;
using mercado.Repositories;
namespace mercado.Service;

public class ProdutoService
{
    private IProdutoRepository _repository;

    private readonly NotificationService _notification;

    public ProdutoService (NotificationService notification, IProdutoRepository repository)
    {
        _notification = notification;
        _repository = repository;
    } 

    public void CadastrarProdutos(string nome, decimal preco, Categoria categoria, int quantidadeEstoque, DateTime dataValidade)
    {

        if (string.IsNullOrWhiteSpace(nome))
            _notification.AdicionarErro("Nome é obrigatório");
            
        if (preco <= 0)
            _notification.AdicionarErro("Preço deve ser maior que zero.");
        
        if (quantidadeEstoque < 0)
            _notification.AdicionarErro("Estoque não pode ser negativo");

        if (_repository.BuscarPorNome(nome) != null)
            _notification.AdicionarErro("Já existe um produto com esse nome");

        if(_notification.TemErros())
            return;

        Produto produto = new Produto(nome, preco, categoria, quantidadeEstoque, dataValidade);
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
