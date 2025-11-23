ABreak - Sistema de Gerenciamento de Pausas
Sistema web para gerenciamento de pausas e intervalos de usuários, desenvolvido em ASP.NET Core com Clean Architecture.

Órgãos transversais
.NET 8.0
Entity Framework Core
SQLite
Swagger/OpenAPI
Estrutura do Projeto
ABreak/
├── ABreak.Domain/          # Entidades e interfaces
├── ABreak.Application/     # Serviços e DTOs
├── ABreak.Infrastructure/  # Repositórios e banco de dados
└── ABreak.Web/            # Controllers e Views
Entidades
Usuário: Usuários do sistema
Pausa: Registro de pausas com início, fim e duração
TipoPausa: Tipos de pausas disponíveis
ConfiguraçãoPausa: Configurações personalizadas
Notificação: alertas de pausa
Instalação
Clonar onte
Restaurante como partes:
dotnet restore
Configure o banco de dados:
cd ABreak.Web
dotnet ef database update
Executar:
dotnet run
:
Web: https://localhost:----
API: https://localhost:----/swagger
Banco de Dados
SQLite configurado em appsettings.json:

{
  "ConnectionStrings": {
    "DefaultConnection": "Data Source=abreak.db"
  }
}
Comandos de Migração
Criar migração:

dotnet ef migrations add NomeDaMigracao --project ABreak.Infrastructure --startup-project ABreak.Web
Aplicar migração:

dotnet ef database update --project ABreak.Infrastructure --startup-project ABreak.Web
Pontos de extremidade da API
Usuários
GET /api/usuários
GET /api/usuarios/{id}
POST /api/usuários
PUT /api/usuarios/{id}
EXCLUIR /api/usuarios/{id}
Pausas
GET /api/pausas
GET /api/pausas/{id}
POST /api/pausas
PUT /api/pausas/{id}
EXCLUIR /api/pausas/{id}
Tipos de Pausa
GET /api/tipospausa
GET /api/tipospausa/{id}
POST /api/tipospausa
PUT /api/tipospausa/{id}
EXCLUIR /api/tipospausa/{id}
configurações
GET /api/configurações
GET /api/configuracoes/{id}
POST /api/configurações
PUT /api/configuracoes/{id}
EXCLUIR /api/configuracoes/{id}
Interface Web
Lar
Usuários
Pausas
Tipos de Pausa
configurações
Arquitetura
Domínio
Entidades de negócio e interfaces dos repositórios.

Aplicativo
Lógica de negócio, serviços, DTOs e ViewModels.

Infraestrutura
Persistência de dados e implementação de repositórios.

Web
Controladores MVC/API e Views Razor.

Padrões Utilizados
Arquitetura limpa
Padrão de Repositório
Camada de serviço
Injeção de Dependência
Code First (Entity Framework)
Projeto desenvolvido para Solução Global.
