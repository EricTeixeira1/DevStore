using DevStore.Domain.Models;
using DevStore.Infra.Data.Repositories;
using DevStore.Infra.Data.Context;
using DevStore.SharedKernel.Data;
using Microsoft.EntityFrameworkCore;
using DevStore.Application.Commands;

namespace DevStore.WebApp.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add app context
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            // Add repositórios
            builder.Services.AddScoped<IRepository<Produto>, ProdutoRepository>();
            builder.Services.AddScoped<IRepository<Pedido>, PedidoRepository>();

            // Register MediatR services
            builder.Services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(typeof(ProdutoCommandHandler).Assembly);
            });

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
