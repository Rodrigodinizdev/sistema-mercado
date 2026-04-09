namespace mercado.Service;

public class NotificationService
{
    private List<string> _erros = [];

    public void AdicionarErro(string mensagem) => _erros.Add(mensagem);
    public bool TemErros() => _erros.Count > 0;
    public List<string> GetErros() => _erros;

    public void Limpar() => _erros.Clear();

    public void ExibirErros()
    {
        foreach (var erro in _erros)
            Console.WriteLine($"ERRO: {erro}");

        Limpar();
    }

    internal bool HasErros()
    {
        throw new NotImplementedException();
    }
}
