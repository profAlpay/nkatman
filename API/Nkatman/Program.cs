

using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using nkatman.Core.Repositories;
using nkatman.Core.Services;
using nkatman.Core.UnitOfWorks;
using nkatman.Repository;
using nkatman.Repository.Repositories;
using nkatman.Repository.UnitOfWorks;
using nkatman.Service.Mappings;
using nkatman.Service.Services;
using Nkatman.API.Filters;
using Nkatman.API.Middlewares;
using Nkatman.API.Modules;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
  options =>
  {
      options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
      {
          ValidateAudience = true,
          ValidateIssuer = true,
          ValidateIssuerSigningKey = true,
          ValidAudience = builder.Configuration["Token:Audience"],
          ValidIssuer = builder.Configuration["Token:Issuer"],
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
          ValidateLifetime = false,
          ClockSkew = TimeSpan.Zero
      };
  }
    );

// rate limiter

// add output cache

// ToDo fjfjfmfff


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped(typeof(NotFoundFilter<>));

builder.Services.AddTransient<IcustomerService, CustomerService>();
builder.Services.AddTransient<ISaleService, SaleService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IRoleService, RoleService>();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddTransient<IPaymentService, PaymentService>();
builder.Services.AddTransient<IGroupService, GroupService>();
builder.Services.AddTransient<IGroupInRoleService, GroupInRoleService>();
builder.Services.AddTransient<IDepartmentService, DepartmentService>();

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IUnitOfWorks, UnitOfWorks>();

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IGroupInRoleRepository, GroupInRoleRepository>();
builder.Services.AddTransient<IRoleRepository, RoleRepository>();
builder.Services.AddTransient<IGroupRepository, GroupRepository>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ISaleRepository, SaleRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();

builder.Services.AddTransient<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddTransient<ITokenHandler, MyTokenHandler>();

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
