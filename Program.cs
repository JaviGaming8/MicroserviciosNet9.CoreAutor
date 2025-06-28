using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Tienda.Microservicios.Autor.Api.Persistencia;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<ContextoAutor>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Configurar CORS para permitir solicitudes desde React (y otros orígenes)
builder.Services.AddCors(options =>
{
    options.AddPolicy("PermitirTodo", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Tienda.Microservicios.Autor.Api v1");
        c.RoutePrefix = "";
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();

// Aplica CORS justo después de HTTPS y antes de Authorization y MapControllers
app.UseCors("PermitirTodo");

app.UseAuthorization();

app.MapControllers();

app.Run();
