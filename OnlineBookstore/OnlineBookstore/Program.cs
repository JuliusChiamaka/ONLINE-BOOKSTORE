using Microsoft.EntityFrameworkCore;
using OnlineBookstore.Application.Interfaces;
using OnlineBookstore.Application.Repositories;
using OnlineBookstore.Application.Services;
using OnlineBookstore.Infrastructure.Data;
using OnlineBookstore.Service.Contract;
using OnlineBookstore.Service.Contract.Base;
using OnlineBookstore.Service.Contract.Repository;
using OnlineBookstore.Service.Implementation;

namespace OnlineBookstore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddControllers();

            //builder.Services.AddDbContext<DapperDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddSingleton<DapperDbContext>();
            builder.Services.AddScoped<IPaymentService, PaymentService>();
            builder.Services.AddScoped<IPurchaseHistoryRepository, PurchaseHistoryRepository>();
            builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
            builder.Services.AddScoped<IBooksRepository, BooksRepository>();

            builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
            builder.Services.AddScoped<IBookservice, Bookservice>();
            builder.Services.AddScoped<IPurchaseHistoryService, PurchaseHistoryService>();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}