namespace mercado.Models;

public class Produto
{
    public Produto(string nome, decimal preco, Categoria categoria, int quantidadeEstoque, DateTime dataValidade)
    {
        Id = ++ContadorId;
        Nome = nome;
        Preco = preco;
        Categoria = categoria;
        QuantidadeEstoque = quantidadeEstoque;
        DataValidade = dataValidade;
    }
    private static int ContadorId = 0;
    public int Id { get; }
    public string Nome { get; private set; }
    public Categoria Categoria { get; private set; }
    public decimal Preco { get; private set; }
    public int QuantidadeEstoque { get; private set; }
    public DateTime DataValidade { get; private set; }

    public void DebitarEstoque(int quantidade)
    {
        if (QuantidadeEstoque < quantidade)
        {
            Console.WriteLine("Estoque insuficiente!");
            return;
        }

        QuantidadeEstoque -= quantidade;
    }

    public void AdcionarEstoque(int quantidade)
    {
        QuantidadeEstoque += quantidade;
    }

    public override string ToString()
    {
        return $"Produto: {Nome} | Categoria: {Categoria} | Preco: {Preco:C} | Estoque: {QuantidadeEstoque} unidades | Data Validade: {DataValidade}";
    }

}
