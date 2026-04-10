using mercado.Service;
using mercado.DTOs;
namespace mercado.UI;

public class CategoriaUI
{
    private readonly CategoriaService _service;
    private readonly NotificationService _notification;

    public CategoriaUI(CategoriaService service, NotificationService notification)
    {
        _service = service;
        _notification = notification;
    }

    public void Cadastrar()
    {
        Console.Clear();
        Console.Write("Nome: ");
        string nome = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(nome))
        {
            Console.WriteLine("ERRO! O nome não pode ser vazio. Tente novamente.");
            Console.Write("Nome: ");
            nome = Console.ReadLine();
        }

        Console.Write("Descrição: ");
        string descricao = Console.ReadLine();
        while (string.IsNullOrWhiteSpace(descricao))
        {
            Console.WriteLine("ERRO! A descrição não pode ser vazia. Tente novamente.");
            Console.Write("Descrição: ");
            descricao = Console.ReadLine();
        }

        var dto = new CriarCategoriaDto
        {
            Nome = nome,
            Descricao = descricao,
        };

        _service.CadastrarCategoria(dto);

        if (_notification.HasErros())
            _notification.ExibirErros();

    }

    public void Listar()
    {
        Console.Clear();
        _service.ListarCategorias();
        if (_notification.HasErros()) _notification.ExibirErros();
    }
}
