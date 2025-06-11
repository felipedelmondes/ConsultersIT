using ConsultersIT.Core.Interfaces;
using ConsultersIT.Core.Services;
using ConsultersIT.Infra.Data.Context;
using ConsultersIT.Infra.Interfaces;
using ConsultersIT.Infra.Repositories;
using ICepResponse = ConsultersIT.Common.Interfaces.ICepResponse;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

// Configuração da autenticação JWT
var jwtSecret = Environment.GetEnvironmentVariable("JWT_SECRET") ?? "S16D/psC/ljNyi3zEGq9GpgILIwwPi+mnIXYPtJ7nUI=";

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = false, // Ajuste conforme sua necessidade
        ValidateAudience = false, // Ajuste conforme sua necessidade
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(jwtSecret))
    };
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "ConsultersIT API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando o esquema Bearer. Exemplo: 'Bearer {seu token}'",
        Name = "Authorization",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
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
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

// Busca a string de conexão do ambiente (variável de ambiente do pipeline), appsettings, appsettings.Development e secrets
var postgresConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__PostgresDb")
    ?? builder.Configuration.GetConnectionString("PostgresDb");
if (string.IsNullOrEmpty(postgresConnectionString))
{
    throw new InvalidOperationException("A ConnectionString 'PostgresDb' não foi encontrada. Verifique se está definida em appsettings, appsettings.Development, user-secrets ou variável de ambiente ConnectionStrings__PostgresDb.");
}
Console.WriteLine($"String de conexão utilizada: {postgresConnectionString}");

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
app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowAll");

// Mapeando o endpoint de HealthChecks
app.MapHealthChecks("/health");

// Alterando a porta padrão para evitar conflito
app.Run();