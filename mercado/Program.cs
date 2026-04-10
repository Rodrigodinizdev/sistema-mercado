using mercado.Service;
using mercado.Repositories;
using mercado.UI;

NotificationService notification = new NotificationService();

IProdutoRepository produtoRepository = new ProdutoRepository();
ICategoriaRepository categoriaRepository = new CategoriaRepository();
IVendaRepository vendaRepository = new VendaRepository();

ProdutoService produtoService = new ProdutoService(produtoRepository, notification);
CategoriaService categoriaService = new CategoriaService(categoriaRepository, notification);
VendaService vendaService = new VendaService(vendaRepository, notification);

ProdutoUI produtoUI = new ProdutoUI(produtoService, categoriaService, notification);
CategoriaUI categoriaUI = new CategoriaUI(categoriaService, notification);
VendaUI vendaUI = new VendaUI(vendaService, produtoService, notification);

while (true)
{
    Console.Clear();
    Console.WriteLine("=== SISTEMA DE MERCADO ===");
    Console.WriteLine("1 - Cadastrar Categoria");
    Console.WriteLine("2 - Cadastrar Produto");
    Console.WriteLine("3 - Realizar Venda");
    Console.WriteLine("4 - Listar Produtos");
    Console.WriteLine("5 - Listar Categorias");
    Console.WriteLine("6 - Listar Vendas");
    Console.WriteLine("7 - Remover Produto");
    Console.WriteLine("0 - Sair");

    Console.Write("\nEscolha: ");
    string opcao = Console.ReadLine();

    Console.Clear();

    switch (opcao)
    {
        case "1": categoriaUI.Cadastrar(); break;
        case "2": produtoUI.Cadastrar(); break;
        case "3": vendaUI.Realizar(); break;
        case "4": produtoUI.Listar(); break;
        case "5": categoriaUI.Listar(); break;
        case "6": vendaUI.Listar(); break;
        case "7": produtoUI.Remover(); break;
        case "0":
            Console.WriteLine("Saindo...");
            return;
        default:
            Console.WriteLine("Opção inválida!");
            break;
    }

    Console.WriteLine("\nPressione qualquer tecla...");
    Console.ReadKey();
}