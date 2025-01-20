[Back to README](../README.md)

### Considerações
- Gostei do uso do Bogus para gerar cenários randômicos
- Sobre o armazenamento do valor unitário do ítem dentro da venda, entendo que isso é apenas um "curto circuito" para o valor do ítem no momento da venda, contudo, o mesmo poderia ser obtido através de uma estrutura de históricos de preços / descontos especiais aplicados ao ítem no momento da compra
- Inclui outros "status" na compra pra representar de forma mais fiel ao encontrado no mercado (é claro que isso deveria ser discutido mas implementei apenas para título de teste técnico)
- Entendo que o status não deveria ser um ítem "atualizavel". O mesmo deve ser alterado à medido que a compra "caminha" pelo workflow do sitema, por esse motivo ele foi removido do "CreateRequest"
- A data da compra também foi removido, pois o mesmo é inferido pelo sistema
- O campo "TotalAmount" tbem é campo calculado no servidor à partir dos preços e descontos aplicados, por esse motivo, é apenas um dado de retorno dos endpoints
- O campo "UnitPrice" tbem é campo de retorno obtido à partir do domínio produto, portanto, ele não faz parte do request
- Estou usando o "ProductId" como "Key" ao criar os itens na venda portanto, produtos que podem ter variações como cor, tamanho, etc, deveriam ser tratados em um segundo momento. Á priore, estou tratando as variações como sendo ProductIds diferentes.
- Adicionei as estruturas referentes ao SaleItem dentro de pastas Sales (ao invés de SaleItem) pois é uma entidade fortemente acoplada, sendo que o SaleItem "não vive" sem a Sale.
- Adicionei um Domain chamado Discount sem validações pois não estamos tratando o CRUD do mesmo
- Defini o "TotalAmount" para um valor que deveria ter sido conversado com a área de negócio contudo, não tenho acesso à mesma
- Verifiquei que deveria existir um endpoint no sistema denominado "Product", o que indica que esse domínio também faz parte do micro-serviço em questão, por esse motivo o criei com as propriedades ID e UnitPrice, apenas para inferir os preços, fazer calculos de desconto e salvar o valor no momento da compra
- A autenticação parece não estar habilitada
- Mantive o BranchId e CustomerId do Sale sem nenhuma ForeigKey pois não encontrei documentação informando que eles fazem parte do domínio do micro-serviço em questão, por esse motivo, é uma chave para outro domínio que apenas foi armazenada no domínio do APP
- A propriedade total amount não está sendo persistida para o SaleItem pois é mesma é auto calculada no dominio à partir da quantidade, unitprice e desconto
- Como eu criei uma estrutura de descontos em banco de dados, a aplicação do mesmo depende do que está persistido, de qualquer forma, criei os registros padrões para o que já estão definidos na documentação
- Particularmente não gosto do mapeamento implicito do auto-mapper. O sistema fica muito suceptível à falhas ocultas
- Não ficou claro se o DELETE Sale deveria excluir logicamente ou fisicamente o registro. Como na documentação existe à referência para o Cancelamento, eu preferi utilizar a exclusao lógica e apenas marcar a Sale como Cancelled
- Está faltando o mapeamento do retorno no GetUserProfile
- Não ficou claro se o GetSale deveria retornar os items também, entendo que isso poderia ser um parametro opcional definido pelo client via "fields" querystring. Decidi não implementar em um primeiro momento.
- Adicionei uma regra onde apenas compras nos estados AwaitingPayment e AwaitingTransport podem ser canceladas (Deletadas)
- Não entendi o que exatamente deve ser atualizado na Sale. Usualmente costuma-se cancelar a venda e fazer uma nova, por esse motivo, não desenvolvi esse endpoint.
- Alguns documentações provenientes de comentários estão errados, no CreateUserHandler, por exemplo, os parâmetros do construtor não "batem" com o comentário
- Padrão de comentário (inglês ou português?) exemplo IUser / User
- Escolha de decimal para os valores em moeda: O decimal é tipo de dados de 128 bits. Comparado ao floar, o tipo decimal possui mais precisão e intervalo menor, o que o torna apropriado para cálculos financeiros e monetários.
- Notei que alguns testes como funcionais e de integração não foram feitos para os endpoints previamente estabelecidos, não sei se isso foi um acordo feito com a área de negócio, portanto, também não os realizei para a funcionalidade que desenvolvi. Vale ressaltar que a estrutura atual excluir os testes das camadas de WebApi e Application