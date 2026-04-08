using mercado.Models;
namespace mercado.Service;

public class CategoriaService
{
    public List<Categoria> listaCategorias = [];

    public void CadastrarCategoria(string nome, string descricao)
    {
        if (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("Nome é obrigatório.");
            return;
        }

        if (string.IsNullOrWhiteSpace(descricao))
        {
            Console.WriteLine("Descrição é obrigatória.");
            return;
        }

        if (listaCategorias.Any(c => c.Nome.Equals(nome, StringComparison.OrdinalIgnoreCase)))
        {
            Console.WriteLine("Já existe uma categoria com esse nome.");
            return;
        }

        Categoria categoria = new Categoria(nome, descricao);
        listaCategorias.Add(categoria);
        Console.WriteLine($"Categoria cadastrada: {categoria.Nome}");
    }

    public void ListarCategorias()
    {
        if (listaCategorias.Count == 0)
        {
            Console.WriteLine("Nenhuma categoria cadastrada.");
            return;
        }

        Console.WriteLine("=== CATEGORIAS ===");
        foreach (var categoria in listaCategorias)
        {
            Console.WriteLine($"ID: {categoria.Id} | Nome: {categoria.Nome} | Descrição: {categoria.Descricao}");
        }
    }

    public void RemoverCategoria(int id)
    {
        var categoria = listaCategorias.FirstOrDefault(c => c.Id == id);

        if (categoria == null)
        {
            Console.WriteLine("Categoria não encontrada.");
            return;
        }

        listaCategorias.Remove(categoria);
        Console.WriteLine($"Categoria '{categoria.Nome}' removida com sucesso.");
    }
}