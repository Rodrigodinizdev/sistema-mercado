
using mercado.Models;

namespace mercado.Interfaces
{
    public interface IVendaRepository
    {
        void Adicionar(Venda venda);
        Venda BuscarPorId(int id);
        List<Venda> ListarTodas();
    }
}