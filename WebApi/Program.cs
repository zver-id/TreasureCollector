using AutoMapper;
using FluentNHibernate.Cfg;
using Microsoft.AspNetCore.Cors.Infrastructure;
using TreasureCollector.Application.Services;
using WebApi.Mappings;

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

    builder.Services.AddSingleton<CoinService>();
    builder.Services.AddSingleton<InventoryReportService>();

    ILoggerFactory loggerFactory = LoggerFactory.Create(logBuilder => logBuilder.AddJsonConsole());

    var mappingConfig = new MapperConfiguration(
      cfg => cfg.AddProfile<ResponseToObjectMappings>(),
      loggerFactory);
    var mapper = mappingConfig.CreateMapper();
    builder.Services.AddSingleton(mapper);
    
    builder.Services.AddControllers();

    builder.Services.AddCors(options =>
    {
      options.AddPolicy(name: corsPolicyName,
        policyBuilder =>
        {
          policyBuilder.AllowAnyOrigin();
          policyBuilder.AllowAnyMethod();
          policyBuilder.AllowAnyHeader();
        });
    });

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