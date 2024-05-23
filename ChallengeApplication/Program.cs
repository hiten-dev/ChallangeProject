using Domain.Models;
using Repository;
using Service.Interface;
using Service;
using Domain.ViewModels;
using Microsoft.EntityFrameworkCore;
using ChallengeApplication.Data;
using NLog.Web;
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options =>
        options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddControllers();

// NLog: Setup NLog
builder.Logging.ClearProviders();
builder.Host.UseNLog();


#region Service Injected
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<ICustomerService<CustomerDetail>, CustomerService>();
builder.Services.AddTransient<IFetchCustomer<Customer>, FetchCustomer>();
#endregion

builder.Services.AddSwaggerGen();
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "Your API v1");
});
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
