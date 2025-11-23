using ApiWMovies.DAL;
using ApiWMovies.MoviesMapper;
using ApiWMovies.Repository;
using ApiWMovies.Repository.IRepository;
using ApiWMovies.Service;
using ApiWMovies.Service.IService;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(x => x.AddProfile<Mappers>());

//Inject the Repository Layer

builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();

//Inject the Service Layer

builder.Services.AddScoped<ICategoryService, CategoryService>();

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnection")));

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