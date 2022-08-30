using AutoMapper;
using Microsoft.EntityFrameworkCore;
using UserManager.API.ViewModels;
using UserManager.Domain.Entities;
using UserManager.Infra.Context;
using UserManager.Infra.Interfaces;
using UserManager.Infra.Repositories;
using UserManager.Services.DTO;
using UserManager.Services.Interfaces;
using UserManager.Services.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region AutoMapper
var autoMapperConfig = new MapperConfiguration(cfg =>
{
    cfg.CreateMap<User, UserDTO>().ReverseMap();
    cfg.CreateMap<CreateUserViewModel,UserDTO>().ReverseMap();

});
builder.Services.AddSingleton(autoMapperConfig.CreateMapper());

#endregion

#region DI
builder.Services.AddSingleton(d=>builder.Configuration);
builder.Services.AddDbContext<UserManagerContext>(o => o.UseSqlServer(builder.Configuration["ConnectionStrings:Default"]),ServiceLifetime.Transient);
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
#endregion

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
