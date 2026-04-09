using mercado.Models;
namespace mercado.Repositories;

    public interface ICategoriaRepository
    {
        void Adicionar(Categoria categoria);
        Categoria BuscarPorId(int id);
        Categoria BuscarPorNome(string nome);
        List<Categoria> ListarTodas();
        void Remover(Categoria categoria);
    }
