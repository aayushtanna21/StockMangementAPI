using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using StockMangementAPI.Data.Admin;
using StockMangementAPI.Data.User;
using StockMangementAPI.Models;
using System.Reflection;
using System.Text;
namespace StockMangementAPI
{
    public class Program
	{
		private static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			builder.Services.AddCors(options =>
			{
				options.AddPolicy("AllowAll", policy =>
				{
					policy.AllowAnyOrigin()
						  .AllowAnyMethod()
						  .AllowAnyHeader();
				});
			});

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
            builder.Services.AddScoped<UserSalesRepository>();
            builder.Services.AddScoped<UserSalesReturnRepository>();
            builder.Services.AddScoped<UserPurchaseRepository>();
            builder.Services.AddScoped<UserPurchaseReturnRepository>();
            builder.Services.AddControllers()
				.AddFluentValidation(fv =>
					fv.RegisterValidatorsFromAssemblies(new[] { Assembly.GetExecutingAssembly() }));
			builder.Services.AddAuthentication(options =>
			{
				options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			}).AddJwtBearer(options =>
			{
				options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
				{
					ValidateIssuer = true,
					ValidateAudience = true,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					ValidIssuer = builder.Configuration["Jwt:Issuer"],
					ValidAudience = builder.Configuration["Jwt:Audience"],
					IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
				};
			});
			var app = builder.Build();
			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseHttpsRedirection();

			app.UseAuthentication();
			app.UseAuthorization();

			app.MapControllers();

			app.Run();
		}
	}
}