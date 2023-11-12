using Customer.Domain.Repositories;
using Customer.Domain.Services;
using Customer.Infrastructure;
using Customer.Infrastructure.repositories;
using Customer.Web.Api.Abstractions;
using Customer.Web.Api.ActionFilter;
using Customer.Web.Api.Configurations;
using Customer.Web.Api.Services;
using Customer.Web.Api.Validation;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new ArgumentException("The connectionString is null");

builder.Services.AddSingleton(new SqlConnectionFactory(connectionString));
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddScoped<ILinkService, LinkService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options => options.Filters.Add<AppExceptionFilter>());
builder.Services.AddValidators();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthCheckConfigurations(connectionString);


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseHealthCheckConfigurations();

app.MapControllers();
app.Run();

public partial class Program
{
}