namespace mercado.DTOs;
using mercado.Models;

    public class CriarVendaDto
    {
        public List<(Produto Produto, int Quantidade)> Itens { get; set; } = [];
    }
