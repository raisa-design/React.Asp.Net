using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using ProAtividade.API.Data;

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

