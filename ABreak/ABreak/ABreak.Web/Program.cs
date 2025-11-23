using ABreak.Application.Interfaces;
using ABreak.Application.Services;
using ABreak.Domain.Interfaces;
using ABreak.Infrastructure.Data;
using ABreak.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configurar o DbContext com SQLite
builder.Services.AddDbContext<ABreakDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar os repositórios
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IPausaRepository, PausaRepository>();
builder.Services.AddScoped<ITipoPausaRepository, TipoPausaRepository>();
builder.Services.AddScoped<IConfiguracaoPausaRepository, ConfiguracaoPausaRepository>();

// Registrar os services
builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IPausaService, PausaService>();
builder.Services.AddScoped<ITipoPausaService, TipoPausaService>();
builder.Services.AddScoped<IConfiguracaoPausaService, ConfiguracaoPausaService>();

// Adicionar suporte a API Controllers
builder.Services.AddControllers();

// Adicionar Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// Habilitar Swagger em desenvolvimento
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Rotas para MVC
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Rotas para API
app.MapControllers();

app.Run();