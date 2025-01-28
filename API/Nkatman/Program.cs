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

var app = builder.Build();

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
