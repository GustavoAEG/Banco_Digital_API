# Conta Corrente API

API desenvolvida para simular movimentações bancárias com controle de transferências, autenticação JWT, idempotência e tarifação.

---

## Tecnologias Utilizadas

- ASP.NET Core 8
- DDD (Domain-Driven Design)
- CQRS + MediatR
- Entity Framework Core + SQLite
- Swagger (OpenAPI)
- Docker + Docker Compose
- Testes com xUnit
- JWT Authentication

---

## ⚙️ Endpoints principais

- `POST /api/conta` – Criar conta
- `POST /api/login` – Login e geração de token
- `POST /api/transferencia` – Efetuar transferência
- `GET /api/extrato` – Consultar extrato e saldo
---

## 🐳 Como rodar com Docker

1. Clonar o projeto
2. Rodar o comando:

```bash
docker-compose up --build

Acessar:

bash
Copiar
Editar
http://localhost:5000/swagger

Executar Testes

dotnet test

✅ Status
✅ Funcionalidades completas
✅ Swagger funcionando
✅ Testes automatizados
✅ Docker OK

Desenvolvedor
Gustavo Gonçalves
🔗 LinkedIn
