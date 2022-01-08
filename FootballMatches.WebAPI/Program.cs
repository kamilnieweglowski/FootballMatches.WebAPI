using AutoMapper;
using FootballMatches.Infrastructure.Data;
using FootballMatches.WebAPI.Mappings;
using Microsoft.EntityFrameworkCore;

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
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer("name=ConnectionStrings:DefaultConnection"));
    //opt.UseSqlite($"Data Source=./../FootballMatches.Infrastructure/Data/footballmatches.db"));
    //opt.UseInMemoryDatabase("FootballMatchesDB"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
