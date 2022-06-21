using LocationLibrary.BusinessLogic;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text.Json.Serialization;

using NLog.Web;

var builder = WebApplication.CreateBuilder(args);

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
builder.Host.UseNLog();

// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles); ;

// https://www.geekinsta.com/mysql-with-net-core-and-entity-framework/
// https://arjavdave.com/2022/04/17/code-first-entity-framework-core-mysql/

// Dependency Injection
/*builder.Services.AddDbContext<rhlocationContext>(x => 
    {
        var connectionString = builder.Configuration.GetConnectionString("RHLocationDatabase");
        x.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
    });*/

builder.Services.AddScoped<ILocationService, LocationService>();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// Voir https://docs.microsoft.com/fr-fr/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-6.0&tabs=visual-studio
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "WSDatabaseCore API",
        Version = "V1",
        Description = "API de gestion des locations en ASP.Net Core Web API",
        Contact = new OpenApiContact()
        {
            Name = "Jean-Luc Bompard",
            Email = "jlb.epsi@gmail.com"
        }
    });

    // Ajout des commentaires XML
    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();


app.UseRouting();

// Enabling CORS
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    app.UseSwaggerUI(c =>
    {
        // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
        // specifying the Swagger JSON endpoint : v1 est le nom défini avec SwaggerDoc (voir ConfigureServices)
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Gestions des locations V1");
    });
}


app.Run();
