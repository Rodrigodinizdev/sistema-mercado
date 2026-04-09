using mercado.Models;
namespace mercado.Repositories
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private List<Categoria> _categorias = [];
        void ICategoriaRepository.Adicionar(Categoria categoria) => _categorias.Add(categoria);
        

        Categoria ICategoriaRepository.BuscarPorId(int id) => _categorias.FirstOrDefault(c => c.Id == id);
        

        Categoria ICategoriaRepository.BuscarPorNome(string nome) => _categorias.FirstOrDefault(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase));
       

        List<Categoria> ICategoriaRepository.ListarTodas() => _categorias;
       

        void ICategoriaRepository.Remover(Categoria categoria) => _categorias.Remove(categoria);
    }
}