using mercado.Models;
namespace mercado.Repositories;

public class ProdutoRepository : IProdutoRepository
{
    private List<Produto> _produtos = [];

    void IProdutoRepository.Adicionar(Produto produto) => _produtos.Add(produto);

    Produto IProdutoRepository.BuscarPorId(int id) => _produtos.FirstOrDefault(p => p.Id == id);

    Produto IProdutoRepository.BuscarPorNome(string nome) => _produtos.FirstOrDefault(p => p.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));

    List<Produto> IProdutoRepository.ListarTodos() => _produtos;
    void IProdutoRepository.Remover(Produto produto) => _produtos.Remove(produto);
  
}
