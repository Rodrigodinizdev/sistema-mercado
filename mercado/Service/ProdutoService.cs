using mercado.Models;
namespace mercado.Service;

public class ProdutoService
{
    public List<Produto> listaProdutos = [];

    public void CadastrarProdutos(string nome, decimal preco, Categoria categoria, int quantidadeEstoque, DateTime dataValidade)
    {

        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome é obrigatório");
            return;
        }

        if (preco <= 0)
        {
            Console.WriteLine("Preço deve ser maior que zero");
            return;
        }

        if (quantidadeEstoque < 0)
        {
            Console.WriteLine("Estoque não pode ser negativo");
            return;
        }

        if (listaProdutos.Any(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("ERRO! Já existe um produto com esse nome");
            return;
        }

        Produto produto = new Produto(nome, preco, categoria, quantidadeEstoque, dataValidade);

        listaProdutos.Add(produto);

        Console.WriteLine($"Produto Cadastrado: {produto}");
    }

    public void ListarProdutos()
    {
        if (listaProdutos.Count == 0)
        {
            Console.WriteLine("Nenhum produto cadastrado.");
            return;
        }

        Console.WriteLine("=== PRODUTOS ===");
        foreach (var produto in listaProdutos)
        {
            Console.WriteLine(produto);
        }
    }

    public void RemoverProduto(int id)
    {
        var produto = listaProdutos.FirstOrDefault(p => p.Id == id);

        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado.");
            return;
        }

        listaProdutos.Remove(produto);
        Console.WriteLine($"Produto '{produto.Nome}' removido com sucesso.");
    }
}
