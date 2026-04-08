using mercado.Service;
using mercado.Models;

ProdutoService produtoService = new ProdutoService();
CategoriaService categoriaService = new CategoriaService();
VendaService vendaService = new VendaService();

while (true)
{
    Console.Clear();
    Console.WriteLine("=== SISTEMA DE MERCADO ===");
    Console.WriteLine("1 - Cadastrar Categoria");
    Console.WriteLine("2 - Cadastrar Produto");
    Console.WriteLine("3 - Realizar Venda");
    Console.WriteLine("4 - Listar Produtos");
    Console.WriteLine("5 - Listar Categorias");
    Console.WriteLine("6 - Remover Produto");
    Console.WriteLine("0 - Sair");

    Console.Write("\nEscolha: ");
    string opcao = Console.ReadLine();

    Console.Clear();

    switch (opcao)
    {
        case "1":
            CadastrarCategoria(categoriaService);
            break;

        case "2":
            CadastrarProduto(produtoService, categoriaService);
            break;

        case "3":
            RealizarVenda(vendaService, produtoService);
            break;

        case "4":
            produtoService.ListarProdutos();
            break;

        case "5":
            categoriaService.ListarCategorias();
            break;

        case "6":
            RemoverProduto(produtoService);
            break;

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

static void CadastrarCategoria(CategoriaService service)
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

    service.CadastrarCategoria(nome, descricao);
}

static void CadastrarProduto(ProdutoService produtoService, CategoriaService categoriaService)
{
    Console.Clear();

    if (categoriaService.listaCategorias.Count == 0)
    {
        Console.WriteLine("Cadastre pelo menos uma categoria antes de cadastrar um produto.");
        return;
    }

    Console.Write("Nome: ");
    string nome = Console.ReadLine();
    while (string.IsNullOrWhiteSpace(nome))
    {
        Console.WriteLine("ERRO! O nome não pode ser vazio. Tente novamente.");
        Console.Write("Nome: ");
        nome = Console.ReadLine();
    }

    decimal preco = 0;
    Console.Write("Preço: ");
    while (!decimal.TryParse(Console.ReadLine(), out preco) || preco <= 0)
    {
        Console.WriteLine("ERRO! Digite um preço válido maior que zero.");
        Console.Write("Preço: ");
    }

    int estoque = 0;
    Console.Write("Quantidade em estoque: ");
    while (!int.TryParse(Console.ReadLine(), out estoque) || estoque < 0)
    {
        Console.WriteLine("ERRO! Digite uma quantidade válida.");
        Console.Write("Quantidade em estoque: ");
    }

    Console.Write("Data de validade (dd/MM/yyyy): ");
    DateTime dataValidade;
    while (!DateTime.TryParse(Console.ReadLine(), out dataValidade) || dataValidade <= DateTime.Now)
    {
        Console.WriteLine("ERRO! Digite uma data válida e futura.");
        Console.Write("Data de validade (dd/MM/yyyy): ");
    }

    categoriaService.ListarCategorias();
    int idCategoria = 0;
    Console.Write("\nID da categoria: ");
    while (!int.TryParse(Console.ReadLine(), out idCategoria) || idCategoria <= 0)
    {
        Console.WriteLine("ERRO! Digite um ID válido.");
        Console.Write("\nID da categoria: ");
    }

    var categoria = categoriaService.listaCategorias.FirstOrDefault(c => c.Id == idCategoria);
    if (categoria == null)
    {
        Console.WriteLine("Categoria não encontrada!");
        return;
    }

    produtoService.CadastrarProdutos(nome, preco, categoria, estoque, dataValidade);
}

static void RealizarVenda(VendaService vendaService, ProdutoService produtoService)
{
    Console.Clear();

    if (produtoService.listaProdutos.Count == 0)
    {
        Console.WriteLine("Nenhum produto cadastrado.");
        return;
    }

    List<(Produto produto, int quantidade)> itens = [];

    while (true)
    {
        produtoService.ListarProdutos();

        Console.Write("\nID do produto (0 para finalizar): ");
        int idProduto;
        while (!int.TryParse(Console.ReadLine(), out idProduto) || idProduto < 0)
        {
            Console.WriteLine("ERRO! Digite um ID válido.");
            Console.Write("\nID do produto (0 para finalizar): ");
        }

        if (idProduto == 0) break;

        var produto = produtoService.listaProdutos.FirstOrDefault(p => p.Id == idProduto);
        if (produto == null)
        {
            Console.WriteLine("Produto não encontrado!");
            continue;
        }

        int quantidade;
        Console.Write("Quantidade: ");
        while (!int.TryParse(Console.ReadLine(), out quantidade) || quantidade <= 0)
        {
            Console.WriteLine("ERRO! Digite uma quantidade válida.");
            Console.Write("Quantidade: ");
        }

        itens.Add((produto, quantidade));
        Console.WriteLine($"{produto.Nome} adicionado à venda.");
    }

    if (itens.Count == 0)
    {
        Console.WriteLine("Nenhum item adicionado. Venda cancelada.");
        return;
    }

    vendaService.RealizarVenda(itens);
}

static void RemoverProduto(ProdutoService produtoService)
{
    Console.Clear();

    if (produtoService.listaProdutos.Count == 0)
    {
        Console.WriteLine("Nenhum produto cadastrado.");
        return;
    }

    produtoService.ListarProdutos();

    int id = 0;
    Console.Write("\nID do produto a remover: ");
    while (!int.TryParse(Console.ReadLine(), out id) || id <= 0)
    {
        Console.WriteLine("ERRO! Digite um número válido.");
        Console.Write("\nID do produto a remover: ");
    }

    produtoService.RemoverProduto(id);
}