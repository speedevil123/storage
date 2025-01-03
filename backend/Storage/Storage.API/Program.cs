using Microsoft.EntityFrameworkCore;
using Storage.Application.Services;
using Storage.Core.Abstractions;
using Storage.DataAccess;
using Storage.DataAccess.Repositories;
using Storage.Infrastructure.Repositories;
//using Storage.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StorageDbContext>(
    options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
    });

builder.Services.AddScoped<IToolsService, ToolsService>();
builder.Services.AddScoped<IToolsRepository, ToolsRepository>();

builder.Services.AddScoped<IWorkersService, WorkersService>();
builder.Services.AddScoped<IWorkersRepository, WorkersRepository>();

builder.Services.AddScoped<IRentalsService, RentalsService>();
builder.Services.AddScoped<IRentalsRepository, RentalsRepository>();

builder.Services.AddScoped<ICategoriesService, CategoriesService>();
builder.Services.AddScoped<ICategoriesRepository, CategoriesRepository>();

builder.Services.AddScoped<IDepartmentsService, DepartmentsService>();
builder.Services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();

builder.Services.AddScoped<IManufacturersService, ManufacturersService>();
builder.Services.AddScoped<IManufacturersRepository, ManufacturersRepository>();

builder.Services.AddScoped<IModelsService, ModelsService>();
builder.Services.AddScoped<IModelsRepository, ModelsRepository>();

builder.Services.AddScoped<IPenaltiesService, PenaltiesService>();
builder.Services.AddScoped<IPenaltiesRepository, PenaltiesRepository>();

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

app.UseCors(x =>
{
    x.WithOrigins("http://localhost:3000") // Allow your frontend origin
     .AllowAnyHeader()
     .AllowAnyMethod()
     .AllowCredentials(); // Allow credentials
});

app.Run();
