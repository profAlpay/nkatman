

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using nkatman.Core.Repositories;
using nkatman.Core.Services;
using nkatman.Core.UnitOfWorks;
using nkatman.Repository;
using nkatman.Repository.Repositories;
using nkatman.Repository.UnitOfWorks;
using nkatman.Service;
using nkatman.Service.Mappings;
using nkatman.Service.Services;
using Nkatman.API.Controllers;
using Nkatman.API.Filters;
using Nkatman.API.Middlewares;
using Nkatman.API.Modules;



var builder = WebApplication.CreateBuilder(args);



//add jwt bearer - appsetting

// rate limiter

// add output cache



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(NotFoundFilter<>));
builder.Services.AddTransient<IcustomerService, CustomerService>();

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IUnitOfWorks, UnitOfWorks>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();


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

app.UseAuthentication();
app.UseCustomException();

app.UseAuthorization();

app.MapControllers();

app.Run();
