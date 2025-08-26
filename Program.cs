
using chat_realtime_backend.Data;
using Microsoft.EntityFrameworkCore;
using DotNetEnv;

namespace chat_realtime_backend
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            Env.Load(); //Carregando variáveis de ambiente

            // Add services to the container.

            var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING");

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
