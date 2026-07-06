# Sistema de Controle de Gastos Residenciais

Aplicacao full-stack para cadastrar pessoas, registrar receitas/despesas e consultar totais por pessoa e totais gerais. O back-end usa ASP.NET Core Web API com Entity Framework Core e SQLite local. O front-end usa React com TypeScript via Vite.

## Requisitos

- .NET SDK 10
- Node.js 24 ou superior
- npm

No PowerShell deste ambiente, use `npm.cmd` caso a execucao de scripts bloqueie `npm`.

## Como rodar o back-end

```powershell
dotnet restore backend\backend.csproj
dotnet run --project backend\backend.csproj --launch-profile http
```

A API ficara em:

- `http://localhost:5085`
- Swagger: `http://localhost:5085/swagger`

O banco SQLite e criado automaticamente em `backend/controle-gastos.db` quando a API inicia. As migrations tambem sao aplicadas automaticamente na inicializacao.

## Como rodar o front-end

```powershell
cd frontend
npm.cmd install
npm.cmd run dev
```

O front-end ficara em `http://localhost:5173` e consome a API em `http://localhost:5085/api`.

Para apontar para outra URL de API, crie um arquivo `frontend/.env` com:

```env
VITE_API_URL=http://localhost:5085/api
```

## Como resetar o banco

Pare o back-end e remova os arquivos SQLite locais:

```powershell
Remove-Item backend\controle-gastos.db,backend\controle-gastos.db-shm,backend\controle-gastos.db-wal -ErrorAction SilentlyContinue
```

Ao iniciar a API novamente, o banco sera recriado pelas migrations.

## Endpoints principais

- `GET /api/pessoas`
- `POST /api/pessoas`
- `DELETE /api/pessoas/{id}`
- `GET /api/transacoes`
- `POST /api/transacoes`
- `GET /api/totais`

## Decisoes tecnicas

- A regra de menor de idade fica em `TransacaoService`: pessoas com idade menor que 18 podem registrar apenas `Despesa`; tentativa de `Receita` retorna `400 Bad Request`.
- A existencia de `PessoaId` e validada antes de criar transacoes; pessoa inexistente retorna `404 Not Found`.
- A delecao em cascata de transacoes foi configurada via EF Core em `AppDbContext` com `DeleteBehavior.Cascade`.
- O SQLite local nao e versionado. O `.gitignore` bloqueia arquivos `.db`, `bin/`, `obj/`, `node_modules/` e builds do Vite.
- A API retorna erros em um formato simples com `mensagem` e, para validacoes de modelo, `detalhes`.
- O front-end usa `fetch` encapsulado em `src/api.ts`, com exibicao de erros no topo da tela ativa.

## Comandos de verificacao

```powershell
dotnet build backend\backend.csproj
cd frontend
npm.cmd run build
npm.cmd run lint
```
