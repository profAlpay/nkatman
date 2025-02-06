

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using nkatman.Core.UnitOfWorks;
using nkatman.Repository;
using nkatman.Repository.UnitOfWorks;
using nkatman.Service;
using nkatman.Service.Mappings;
using Nkatman.API.Modules;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//add jwt bearer - appsetting

// rate limiter

// add output cache

// appdpcontext  - appsetting

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContextFactory<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("sqlserver")));



// builder.Services.AddScoped(typeof(IUnitOfWorks), typeof(UnitOfWorks));

var app = builder.Build();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
containerBuilder.RegisterModule(new RepoServiceModule()));


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// use authentication

app.UseAuthorization();

app.MapControllers();

app.Run();
