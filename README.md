# ABreak - Sistema de Gerenciamento de Pausas

Sistema web para gerenciamento de pausas desenvolvido em ASP.NET Core 8.0 com Clean Architecture. Oferece interface web e API REST para registro e acompanhamento de pausas de usuários.

## Sobre o Projeto

ABreak é uma aplicação completa que permite registrar, configurar e monitorar diferentes tipos de pausas através de uma interface web intuitiva ou API RESTful. O sistema foi desenvolvido seguindo princípios de Clean Architecture e DDD, garantindo código limpo, testável e de fácil manutenção.

## Tecnologias

- .NET 8.0
- ASP.NET Core MVC
- Entity Framework Core
- SQLite
- Swagger/OpenAPI
- Clean Architecture
- Repository Pattern
- Service Layer Pattern

## Estrutura do Projeto

```
ABreak/
├── ABreak.Domain/          # Entidades e interfaces de domínio
├── ABreak.Application/     # Serviços, DTOs e ViewModels
├── ABreak.Infrastructure/  # Repositórios e acesso a dados
└── ABreak.Web/            # Controllers, Views e configurações
```

## Funcionalidades

- Gerenciamento completo de usuários
- Registro de pausas com início, fim e duração
- Configuração de tipos de pausas personalizados
- Configurações individuais por usuário
- Sistema de notificações
- Interface web responsiva
- API RESTful documentada
- CRUD completo para todas as entidades

## Entidades

- **Usuario:** Gerenciamento de usuários do sistema
- **Pausa:** Registro de pausas realizadas
- **TipoPausa:** Tipos de pausas disponíveis (café, almoço, etc)
- **ConfiguracaoPausa:** Configurações personalizadas por usuário
- **Notificacao:** Sistema de alertas e notificações

## Pré-requisitos

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022, VS Code ou Rider (opcional)
- Git

## Instalação

1. Clone o repositório:
```bash
git clone https://github.com/seu-usuario/ABreak.git
cd ABreak
```

2. Restaure as dependências:
```bash
dotnet restore
```

3. Configure o banco de dados:
```bash
cd ABreak.Web
dotnet ef database update
```

4. Execute a aplicação:
```bash
dotnet run
```

5. Acesse no navegador:
- Interface Web: https://localhost:----
- Documentação API: https://localhost:----/swagger

## Configuração

O banco de dados SQLite está configurado no `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=abreak.db"
  }
}
```

Para usar outro banco de dados, altere a connection string e o provider no `Program.cs`.

## API Endpoints

### Usuários
```
GET    /api/usuarios          Lista todos os usuários
GET    /api/usuarios/{id}     Busca usuário por ID
POST   /api/usuarios          Cria novo usuário
PUT    /api/usuarios/{id}     Atualiza usuário
DELETE /api/usuarios/{id}     Remove usuário
```

### Pausas
```
GET    /api/pausas            Lista todas as pausas
GET    /api/pausas/{id}       Busca pausa por ID
POST   /api/pausas            Registra nova pausa
PUT    /api/pausas/{id}       Atualiza pausa
DELETE /api/pausas/{id}       Remove pausa
```

### Tipos de Pausa
```
GET    /api/tipospausa        Lista tipos de pausa
GET    /api/tipospausa/{id}   Busca tipo por ID
POST   /api/tipospausa        Cria novo tipo
PUT    /api/tipospausa/{id}   Atualiza tipo
DELETE /api/tipospausa/{id}   Remove tipo
```

### Configurações
```
GET    /api/configuracoes        Lista configurações
GET    /api/configuracoes/{id}   Busca configuração por ID
POST   /api/configuracoes        Cria nova configuração
PUT    /api/configuracoes/{id}   Atualiza configuração
DELETE /api/configuracoes/{id}   Remove configuração
```

## Testando a API

### Via Swagger
1. Execute a aplicação
2. Acesse https://localhost:----/swagger
3. Explore e teste os endpoints interativamente

### Via cURL
```bash
# Listar usuários
curl -X GET https://localhost:----/api/usuarios

# Criar usuário
curl -X POST https://localhost:----/api/usuarios \
  -H "Content-Type: application/json" \
  -d '{"nome":"João Silva","email":"joao@email.com"}'
```

### Ferramentas Recomendadas
- Postman
- Insomnia
- Thunder Client (VS Code)

## Migrações do Banco de Dados

Criar nova migração:
```bash
dotnet ef migrations add NomeDaMigracao --project ABreak.Infrastructure --startup-project ABreak.Web
```

Aplicar migrações:
```bash
dotnet ef database update --project ABreak.Infrastructure --startup-project ABreak.Web
```

Remover última migração:
```bash
dotnet ef migrations remove --project ABreak.Infrastructure --startup-project ABreak.Web
```

## Arquitetura

### Domain Layer
- Entidades de negócio
- Interfaces dos repositórios
- Regras de domínio
- Sem dependências externas

### Application Layer
- Serviços de aplicação
- DTOs (Data Transfer Objects)
- ViewModels
- Interfaces de serviços
- Lógica de negócio

### Infrastructure Layer
- Implementação dos repositórios
- Contexto do Entity Framework
- Migrações do banco de dados
- Acesso a dados

### Web Layer
- Controllers MVC
- Controllers API
- Views Razor
- Configuração da aplicação
- Injeção de dependências

## Padrões e Práticas

- **Clean Architecture:** Separação clara de responsabilidades
- **Repository Pattern:** Abstração do acesso a dados
- **Service Layer:** Lógica de negócio centralizada
- **Dependency Injection:** Baixo acoplamento entre componentes
- **DTOs:** Transferência de dados entre camadas
- **Code First:** Modelagem do banco via código
- **RESTful API:** Endpoints seguindo padrões REST

## Desenvolvimento

### Estrutura de uma Funcionalidade

1. **Criar entidade** em `ABreak.Domain/Entities`
2. **Criar interface do repositório** em `ABreak.Domain/Interfaces`
3. **Criar DTO** em `ABreak.Application/DTOs`
4. **Criar interface do serviço** em `ABreak.Application/Interfaces`
5. **Implementar serviço** em `ABreak.Application/Services`
6. **Implementar repositório** em `ABreak.Infrastructure/Repositories`
7. **Atualizar DbContext** em `ABreak.Infrastructure/Data`
8. **Criar migration:** `dotnet ef migrations add NomeDaFeature`
9. **Criar controller** em `ABreak.Web/Controllers`
10. **Registrar dependências** em `Program.cs`

### Exemplo de Código

```csharp
// Entidade
public class Pausa
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public DateTime DataHoraInicio { get; set; }
    public DateTime? DataHoraFim { get; set; }
    public bool Completada { get; set; }
}

// Interface do Repositório
public interface IPausaRepository : IRepository<Pausa>
{
    Task<IEnumerable<Pausa>> GetByUsuarioIdAsync(int usuarioId);
}

// Service
public class PausaService : IPausaService
{
    private readonly IPausaRepository _repository;
    
    public PausaService(IPausaRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<PausaDTO> CreateAsync(PausaViewModel model)
    {
        // Implementação
    }
}
```

## Contribuindo

1. Faça um fork do projeto
2. Crie uma branch para sua feature (`git checkout -b feature/MinhaFeature`)
3. Commit suas mudanças (`git commit -m 'Adiciona nova feature'`)
4. Push para a branch (`git push origin feature/MinhaFeature`)
5. Abra um Pull Request

## Roadmap

- [ ] Autenticação e autorização
- [ ] Dashboard com estatísticas
- [ ] Relatórios em PDF
- [ ] Notificações em tempo real
- [ ] Integração com calendário
- [ ] App mobile

## Licença

Este projeto é um trabalho acadêmico desenvolvido para fins educacionais.

## Autores

Projeto desenvolvido como parte da Global Solution.

## Contato
fe.felipe2283@gmail.com
Para dúvidas, sugestões ou contribuições, abra uma issue no GitHub.
