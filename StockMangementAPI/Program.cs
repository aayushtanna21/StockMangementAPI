using FluentValidation.AspNetCore;
using StockMangementAPI.Data;
using StockMangementAPI.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<CategoryRepository>();
builder.Services.AddScoped<CustomersRepository>();
builder.Services.AddScoped<SuppliersRepository>();
builder.Services.AddScoped<StockTransactionsRepository>();
builder.Services.AddScoped<BillsRepository>();
builder.Services.AddScoped<BillDetailsRepository>();
builder.Services.AddScoped<PaymentsRepository>();

builder.Services.AddControllers()
	.AddFluentValidation(fv =>
		fv.RegisterValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }));
var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//builder.Services.AddControllers().
//    AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<UserModel>());
//builder.Services.AddControllers().
//    AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<SuppliersModel>());
//builder.Services.AddControllers().
//    AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<StockTransactionsModel>());
//builder.Services.AddControllers().
//    AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductModel>());
//builder.Services.AddControllers().
//    AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<PaymentsModel>());
//builder.Services.AddControllers().
//    AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CustomersModel>());
//builder.Services.AddControllers().
//    AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CategoryModel>());
//builder.Services.AddControllers().
//    AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BillsModel>());
//builder.Services.AddControllers().
//    AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<BillDetailsModel>());
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
