using CustomerApi.Application.Dtos;
using CustomerApi.Application.Services;
using CustomerApi.Application.Validators;
using CustomerApi.Application.Validators.Business.Interface;
using CustomerApi.Application.Validators.Business;
using CustomerApi.Infrastructure.Persistence;
using CustomerApi.Infrastructure.Persistence.Interfaces;
using FluentValidation;
using CustomerApi.Application.Services.Interfaces;
using CustomerApi.Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure EF Core with SQLite
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite")));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    var xmlFilename = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

// Dependencies
builder.Services.AddScoped<IValidator<CustomerDto>, CustomerDtoValidator>();
builder.Services.AddScoped<IValidator<List<CustomerDto>>, CustomerListValidator>();
builder.Services.AddScoped<ICustomerBusinessRules, CustomerBusinessRules>();

// Storage appsettings.json
builder.Services.Configure<StorageOptions>(builder.Configuration.GetSection("Storage"));

// Repositories
builder.Services.AddScoped<ICustomerRepository, SQLiteCustomerRepository>();

//Services
builder.Services.AddScoped<ICustomerService, CustomerService>();

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
