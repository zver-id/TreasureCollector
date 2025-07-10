using Microsoft.AspNetCore.Cors.Infrastructure;
using TreasureCollector.Application.Services;

namespace WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var corsPolicyName = "CorsPolicy";

        // Add services to the container.
        builder.Services.AddAuthorization();

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<ItemsService>();

        builder.Services.AddCors(options =>
        {
            options.AddPolicy(name: corsPolicyName,
                policyBuilder =>
                {
                    policyBuilder.AllowAnyOrigin();
                });
        });

        builder.Services.AddControllers();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();
        app.UseCors(corsPolicyName);
        app.MapControllers();
        
        app.Run();
    }
}