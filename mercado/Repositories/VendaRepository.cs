using mercado.Interfaces;
using mercado.Models;
namespace mercado.Repositories;

public class VendaRepository : IVendaRepository
{
    private List<Venda> _vendas = [];
    void IVendaRepository.Adicionar(Venda venda) => _vendas.Add(venda);
  
    Venda IVendaRepository.BuscarPorId(int id) => _vendas.FirstOrDefault(v => v.Id == id);
   
    List<Venda> IVendaRepository.ListarTodas() => _vendas;
   
}
