using mercado.Models;
namespace mercado.Repositories;

    public interface IVendaRepository
    {
        void Adicionar(Venda venda);
        Venda BuscarPorId(int id);
        List<Venda> ListarTodas();
    }
