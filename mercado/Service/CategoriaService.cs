using mercado.Models;
using mercado.Repositories;
namespace mercado.Service;

public class CategoriaService
{
    private readonly ICategoriaRepository _repository;
    private readonly NotificationService _notification;

    public CategoriaService (NotificationService notification, ICategoriaRepository repository)
    {
        _notification = notification;
        _repository = repository;
    }

    public void CadastrarCategoria(string nome, string descricao)
    {
        if (string.IsNullOrWhiteSpace(nome))
            _notification.AdicionarErro("Nome é obrigatório.");

        if (string.IsNullOrWhiteSpace(descricao))
            _notification.AdicionarErro("Descrição é obrigatória.");

        if (_repository.BuscarPorNome(nome) != null)
            _notification.AdicionarErro("Já existe uma categoria com esse nome.");

        if (_notification.TemErros()) 
            return;

        Categoria categoria = new Categoria(nome, descricao);
        _repository.Adicionar(categoria);
        Console.WriteLine($"Categoria cadastrada: {categoria.Nome} cadastrada com sucesso");
    }

    public void ListarCategorias()
    {
        var categorias = _repository.ListarTodas();

        if (categorias.Count == 0)
        {
            _notification.AdicionarErro("Nenhuma categoria cadastrada.");
            return;
        }

        Console.WriteLine("=== CATEGORIAS ===");
        foreach (var categoria in categorias)
        {
            Console.WriteLine($"ID: {categoria.Id} | Nome: {categoria.Nome} | Descrição: {categoria.Descricao}");
        }
    }

    public void RemoverCategoria(int id)
    {
        var categoria = _repository.BuscarPorId(id);

        if (categoria == null)
        {
            _notification.AdicionarErro("Categoria não encontrada.");
            return;
        }

        _repository.Remover(categoria);
        Console.WriteLine($"Categoria '{categoria.Nome}' removida com sucesso.");
    }

    public Categoria BuscarPorId(int id) => _repository.BuscarPorId(id);
    public List<Categoria> ListarTodas() => _repository.ListarTodas();
}