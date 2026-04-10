namespace mercado.Dtos;
using mercado.Models;

public class CriarProdutoDto
{
    public string Nome { get; set; }
    public decimal Preco { get; set; }
    public int CategoriaId { get; set; }
    public int QuantidadeEstoque { get; set; }
    public DateTime DataValidade { get; set; }
}
