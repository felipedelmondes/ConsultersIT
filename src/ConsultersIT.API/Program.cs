using ConsultersIT.Core.Interfaces;
using ConsultersIT.Core.Services;
using ConsultersIT.Infra.Data.Context;
using ConsultersIT.Infra.Interfaces;
using ConsultersIT.Infra.Repositories;
using ICepResponse = ConsultersIT.Common.Interfaces.ICepResponse;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

//services

// Ajustando os namespaces para resolver ambiguidades
builder.Services.AddTransient<ConsultersIT.Infra.Interfaces.ITesteRepository, TesteRepository>();
builder.Services.AddTransient<ConsultersIT.Core.Interfaces.ITesteService, TesteService>();
builder.Services.AddTransient<ICepResponse, CepService>();
builder.Services.AddTransient<DBContext>();
builder.Services.AddTransient<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddTransient<IUsuariosServices, UsuariosService>();
builder.Services.AddSingleton<IDbConnectionFactory, NpgsqlConnectionFactory>();
builder.Services.AddTransient<IAutenticationService, AutenticationService>();
builder.Services.AddTransient<IJwtService, JwtService>();

// Busca a string de conexão do ambiente (Railway) ou do appsettings.json
var rawDatabaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL")
    ?? Environment.GetEnvironmentVariable("POSTGRES_PRIVATE_URL")
    ?? builder.Configuration.GetConnectionString("PostgresDb");

string postgresConnectionString = rawDatabaseUrl;

// Aceita tanto postgres:// quanto postgresql://
if (!string.IsNullOrEmpty(rawDatabaseUrl) &&
    (rawDatabaseUrl.StartsWith("postgres://") || rawDatabaseUrl.StartsWith("postgresql://")))
{
    // Corrige o prefixo para Uri
    var fixedUrl = rawDatabaseUrl.Replace("postgresql://", "postgres://");
    var uri = new Uri(fixedUrl);
    var userInfo = uri.UserInfo.Split(':');
    if (userInfo.Length != 2)
    {
        throw new InvalidOperationException($"DATABASE_URL mal formatada: '{rawDatabaseUrl}'");
    }
    var builderConn = $"Host={uri.Host};Port={uri.Port};Database={uri.AbsolutePath.TrimStart('/')};Username={userInfo[0]};Password={userInfo[1]};SSL Mode=Require;Trust Server Certificate=true";
    postgresConnectionString = builderConn;
    Console.WriteLine($"String de conexão convertida: {postgresConnectionString}");
}

// Adicionando HealthChecks
builder.Services.AddHealthChecks()
    .AddNpgSql(postgresConnectionString);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.UseHttpsRedirection();

// Mapeando o endpoint de HealthChecks
app.MapHealthChecks("/health");

// Alterando a porta padrão para evitar conflito
app.Run();