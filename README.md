# Engine ETL



### Tecnologias Utilizadas:

  - Web Api Core 2.2
  - EF Core 2.2.6
  - AUTH (JWT)
  - Swagger UI
  - Banco - SQL Server

### Instalação

```sh
$ dotnet restore
$ dotnet ef database update
```


##### Executando o projeto
Já existe um usuário de teste criado, mas outros podem ser adicionados pelo enpoint [https://localhost:5001/api/Authentication/CreateUser]

| usuário | senha |
| ------ | ------ |
| usrteste@gmail.com | 1234|

Como executar:
```sh
$ dotnet run --project EngineETL.API/EngineETL.API.csproj 
```

Após o projeto estar executando abra o Swagger [https://localhost:5001/swagger/index.html]

##### Passo 1: Autenticação
Faça uma chamada ao endpoint [https://localhost:5001/api/Authentication/GetToken] e no response copie o valor de Result.token. 
Nesse response existem dois valores importantes: Result.token e Result.userId

##### Passo 2: Adicionando autenticação no Swagger
Abra o modal de autrorização para colar o token. (https://ibb.co/1MZStjV)]

##### Passo 3: Listando e Salvando Templates
Com o token adicionado você já pode listar os templates salvos fazendo uma chamada a [https://localhost:5001/api/Template/ListTemplates/{userId}]
ou adicionar novos templates [https://localhost:5001/api/Template/SaveTemplate/{userId}]

A regra de mapeamento dos campos para a transformação da msg é sempre [objetoPai.propriedadeAguardada] conforme o json abaixo.

>>{  
     "name":"# Exemplo 1 - Template Estado Rio de Janeiro",
      "propertyCity":"corpo.cidade",
      "cityPropertyName":"cidade.nome",
      "cityPropertyHabitants":"cidade.populacao",
      "propertyNeighborhood":"cidades.bairros.bairro",
      "neighborhoodPropertyName":"bairro.nome",
      "neighborhoodPropertyHabitants":"bairro.populacao"
   }

##### Passo 4: Conversão da msg para o formato único

Com o template esperado já salvo, liste seus templates [https://localhost:5001/api/Template/ListTemplates/{userId}] pegue o id do template na resposta e informe no endpoint [https://localhost:5001/api/Template/ConvertMessage/{templateId}].
Cole a msg xml ou json e execute.

### Todos

 - Escrever testes
 - Utilizar o Docker
