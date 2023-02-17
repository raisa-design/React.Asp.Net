using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProAtividade.Data.Context;
using ProAtividade.Data.Repositories;
using ProAtividade.Domain.Interfaces.Repositories;
using ProAtividade.Domain.Interfaces.Services;
using ProAtividade.Domain.Services;

namespace ProAtividade.API;

public class Startup{

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigurationService(IServiceCollection services)
    {

        services.AddDbContext<DataContext>(
            options => options.UseSqlite(Configuration.GetConnectionString("Default"))
        );

        services.AddScoped<IAtividadeRepo, AtividadeRepo>();
        services.AddScoped<IGeralRepo, GeralRepo>();
        services.AddScoped<IAtividadeService, AtividadeService>();

        // A linha acima diz, toda vez que alguém precisar de um IAtividadeRepo, passe o AtividadeRepo.
        //Como também : Toda vez que alguém precisar do IAtividadeService passe o AtividadeService.
        services.AddControllers()
            .AddJsonOptions( options =>
               {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
               } 
            );
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
    });
     services.AddCors();

    }

    public void Configure(WebApplication app, IWebHostEnvironment environment)
    {
        if (app.Environment.IsDevelopment())
        {
           app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        });
        }

        // app.UseHttpsRedirection();

        app.UseAuthorization();

        app.UseCors(option => option.AllowAnyHeader()
                                    .AllowAnyMethod()
                                    .AllowAnyOrigin());

        app.MapControllers();
            }
}

