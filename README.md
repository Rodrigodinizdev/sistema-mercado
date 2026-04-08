# sistema-mercado

📋 Regra de Negócio — CRUD de Mercado
Entidades principais
# Produto
-Id, Nome, Categoria, Preço, QuantidadeEmEstoque, DataValidade

# Categoria
-Id, Nome, Descrição

# Venda
-Id, DataVenda, ValorTotal, Lista de itens vendidos

# ItemVenda
-Id, Produto, Quantidade, PreçoUnitário (preço no momento da venda)

# Regras
-Cadastro de Produto

-Nome e preço são obrigatórios
-Preço deve ser maior que zero
-Estoque inicial não pode ser negativo
-Não pode cadastrar produto com nome duplicado

# Estoque

-Ao realizar uma venda, o estoque do produto é decrementado automaticamente
-Não é permitido vender quantidade maior do que o estoque disponível
-Produto com estoque zerado é marcado como indisponível

# Venda

-Uma venda deve ter pelo menos um item
-O valor total é calculado automaticamente (soma de quantidade × preço unitário de cada item)
-Não é possível alterar uma venda já finalizada, apenas cancelá-la
-Ao cancelar uma venda, o estoque dos produtos é restaurado

# Validações gerais

-Nenhum campo de texto pode ser nulo ou vazio
-IDs são gerados automaticamente
-Datas são registradas automaticamente pelo sistema
