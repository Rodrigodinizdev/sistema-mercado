using mercado.Models;
namespace mercado.Repositories;

    public interface IProdutoRepository
    {
        void Adicionar(Produto produto);
        Produto BuscarPorId(int id);
        Produto BuscarPorNome(string nome);
        List<Produto> ListarTodos();
        void Remover(Produto produto);
    } 