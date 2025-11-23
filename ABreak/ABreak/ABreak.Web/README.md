# ABreak - Sistema de Gerenciamento de Pausas

Sistema web para gerenciar pausas e intervalos de usuários, desenvolvido em ASP.NET Core com Clean Architecture.

## Tecnologias

- .NET 8.0
- Entity Framework Core
- SQLite
- Swagger/OpenAPI

## Estrutura do Projeto

```
ABreak/
├── ABreak.Domain/          # Entidades e interfaces
├── ABreak.Application/     # Serviços e DTOs
├── ABreak.Infrastructure/  # Repositórios e banco de dados
└── ABreak.Web/            # Controllers e Views
```

## Entidades

- **Usuario:** Usuários do sistema
- **Pausa:** Registro de pausas com início, fim e duração
- **TipoPausa:** Tipos de pausas disponíveis
- **ConfiguracaoPausa:** Configurações personalizadas
- **Notificacao:** Alertas de pausas

## Instalação

1. Clone o repositório
2. Restaure as dependências:
```bash
dotnet restore
```

3. Configure o banco de dados:
```bash
cd ABreak.Web
dotnet ef database update
```

4. Execute:
```bash
dotnet run
```

5. Acesse:
- Web: https://localhost:----
- API: https://localhost:----/swagger

## Banco de Dados

SQLite configurado em `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=abreak.db"
  }
}
```

### Comandos de Migration

Criar migration:
```bash
dotnet ef migrations add NomeDaMigracao --project ABreak.Infrastructure --startup-project ABreak.Web
```

Aplicar migration:
```bash
dotnet ef database update --project ABreak.Infrastructure --startup-project ABreak.Web
```

## API Endpoints

### Usuários
- GET /api/usuarios
- GET /api/usuarios/{id}
- POST /api/usuarios
- PUT /api/usuarios/{id}
- DELETE /api/usuarios/{id}

### Pausas
- GET /api/pausas
- GET /api/pausas/{id}
- POST /api/pausas
- PUT /api/pausas/{id}
- DELETE /api/pausas/{id}

### Tipos de Pausa
- GET /api/tipospausa
- GET /api/tipospausa/{id}
- POST /api/tipospausa
- PUT /api/tipospausa/{id}
- DELETE /api/tipospausa/{id}

### Configurações
- GET /api/configuracoes
- GET /api/configuracoes/{id}
- POST /api/configuracoes
- PUT /api/configuracoes/{id}
- DELETE /api/configuracoes/{id}

## Interface Web

- Home
- Usuários
- Pausas
- Tipos de Pausa
- Configurações

## Arquitetura

### Domain
Entidades de negócio e interfaces dos repositórios.

### Application
Lógica de negócio, serviços, DTOs e ViewModels.

### Infrastructure
Persistência de dados e implementação dos repositórios.

### Web
Controllers MVC/API e Views Razor.

## Padrões Utilizados

- Clean Architecture
- Repository Pattern
- Service Layer
- Dependency Injection
- Code First (Entity Framework)

Projeto desenvolvido para Global Solution.