using AutoMapper;
using FootballMatches.Infrastructure.Data;
using FootballMatches.WebAPI.Mappings;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//add automapper
var mapperConfig = new MapperConfiguration(mc =>
{
    mc.AddProfile(new MappingProfile());
});
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

//add dbContext
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlite($"Data Source=./../FootballMatches.Infrastructure/Data/footballmatches.db"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Football Matches API",
        Description = "A simple ASP.NET Core Web API for dealing with football matches!",
        Contact = new OpenApiContact
        {
            Name = "Kamil Niewêg³owski",
            Email = string.Empty,
            Url = new Uri("https://www.linkedin.com/in/kamil-niew%C4%99g%C5%82owski-067a42b0/"),
        }
    });

    c.ExampleFilters();

    // Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
