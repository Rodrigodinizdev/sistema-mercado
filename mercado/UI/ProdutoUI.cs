using mercado.Service;
using mercado.Dtos;
namespace mercado.UI;

public class ProdutoUI
{
    private readonly ProdutoService _ProdutoService;
    private readonly CategoriaService _categoriaService;
    private readonly NotificationService _notification;

    public ProdutoUI(ProdutoService produtoService, CategoriaService categoriaService, NotificationService notification)
    {
        _ProdutoService = produtoService;
        _categoriaService = categoriaService;
        _notification = notification;
    }

    public void Cadastrar()
    {
        Console.Clear();

        if (_categoriaService.ListarTodas().Count == 0)
        {
            Console.WriteLine("Cadastre pelo menos uma categoria antes de cadastrar um produto.");
            return;
        }

        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("ERRO! O nome não pode ser vazio. Tente novamente.");
            Console.Write("Nome: ");
            nome = Console.ReadLine();
        }

        decimal preco = 0;
        Console.Write("Preço: ");
        while (!decimal.TryParse(Console.ReadLine(), out preco) || preco <= 0)
        {
            Console.WriteLine("ERRO! Digite um preço válido maior que zero.");
            Console.Write("Preço: ");
        }

        int estoque = 0;
        Console.Write("Quantidade em estoque: ");
        while (!int.TryParse(Console.ReadLine(), out estoque) || estoque < 0)
        {
            Console.WriteLine("ERRO! Digite uma quantidade válida.");
            Console.Write("Quantidade em estoque: ");
        }

        Console.Write("Data de validade (dd/MM/yyyy): ");
        DateTime dataValidade;
        while (!DateTime.TryParse(Console.ReadLine(), out dataValidade) || dataValidade <= DateTime.Now)
        {
            Console.WriteLine("ERRO! Digite uma data válida e futura.");
            Console.Write("Data de validade (dd/MM/yyyy): ");
        }

        _categoriaService.ListarCategorias();
        int idCategoria = 0;
        Console.Write("\nID da categoria: ");
        while (!int.TryParse(Console.ReadLine(), out idCategoria) || idCategoria <= 0)
        {
            Console.WriteLine("ERRO! Digite um ID válido.");
            Console.Write("\nID da categoria: ");
        }

        var categoria = _categoriaService.BuscarPorId(idCategoria);
        if (categoria == null)
        {
            Console.WriteLine("Categoria não encontrada!");
            return;
        }

        var dto = new CriarProdutoDto
        {
            Nome = nome,
            Preco = preco,
            QuantidadeEstoque = estoque,
            DataValidade = dataValidade
        };

        _ProdutoService.CadastrarProduto(dto, categoria);

        if (_notification.HasErros())
            _notification.ExibirErros();
    }

    public void Listar()
    {
        Console.Clear();
        _ProdutoService.ListarProdutos();
        if (_notification.HasErros()) _notification.ExibirErros();
    }

    public void Remover()
    {
        Console.Clear();

        if (_ProdutoService.ListarTodos().Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        _ProdutoService.ListarProdutos();

        int id = 0;
        Console.Write("\nID do produto a remover: ");
        while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
        {
            Console.WriteLine("ERRO! Digite um número válido.");
            Console.Write("\nID do produto a remover: ");
        }

        _ProdutoService.RemoverProduto(id);
        if (_notification.HasErros()) _notification.ExibirErros();
    }

}
