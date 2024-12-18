using Microsoft.EntityFrameworkCore;
using Storage.Application.Services;
using Storage.Core.Abstractions;
using Storage.DataAccess;
using Storage.DataAccess.Repositories;
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

//builder.Services.AddScoped<IToolsService, ToolsService>();
//builder.Services.AddScoped<IToolsRepository, ToolsRepository>();

//builder.Services.AddScoped<IWorkersService, WorkersService>();
//builder.Services.AddScoped<IWorkersRepository, WorkersRepository>();

//builder.Services.AddScoped<IRentalService, RentalService>();
//builder.Services.AddScoped<IRentalRepository, RentalRepository>();

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
    x.WithHeaders().AllowAnyHeader();
    x.WithOrigins("http://localhost:3000");
    x.WithMethods().AllowAnyMethod();
});

app.Run();
