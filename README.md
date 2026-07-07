# Sistema de Controle de Gastos Residenciais

Projeto desenvolvido como desafio técnico.

O sistema permite cadastrar pessoas, registrar receitas e despesas e consultar os totais por pessoa e o total geral.

## Tecnologias utilizadas

### Back-end
- ASP.NET Core Web API
- Entity Framework Core
- SQLite

### Front-end
- React
- TypeScript
- Vite

## Como executar o projeto

### Back-end

Na pasta do projeto execute:

```bash
dotnet restore
dotnet run --project backend/backend.csproj
```

A API ficará disponível em:

```
http://localhost:5085
```

Swagger:

```
http://localhost:5085/swagger
```

O banco de dados SQLite é criado automaticamente na primeira execução.

---

### Front-end

Entre na pasta do frontend e execute:

```bash
cd frontend
npm install
npm run dev
```

O projeto ficará disponível em:

```
http://localhost:5173
```

Caso seja necessário alterar a URL da API, crie um arquivo `.env` dentro da pasta `frontend` com:

```env
VITE_API_URL=http://localhost:5085/api
```

## Funcionalidades

- Cadastro de pessoas
- Exclusão de pessoas
- Cadastro de receitas e despesas
- Listagem das transações
- Consulta do total por pessoa
- Consulta do total geral

## Regras de negócio

- Pessoas menores de 18 anos podem registrar apenas despesas.
- Antes de cadastrar uma transação é validado se a pessoa existe.
- Ao excluir uma pessoa, suas transações também são removidas.

## Endpoints

### Pessoas

```
GET    /api/pessoas
POST   /api/pessoas
DELETE /api/pessoas/{id}
```

### Transações

```
GET    /api/transacoes
POST   /api/transacoes
```

### Totais

```
GET    /api/totais
```

## Build

Back-end:

```bash
dotnet build backend/backend.csproj
```

Front-end:

```bash
npm run build
npm run lint
```

## Observações

O projeto utiliza SQLite como banco de dados local.

Caso queira reiniciar o banco, basta excluir o arquivo `controle-gastos.db` da pasta `backend` e executar o projeto novamente.
