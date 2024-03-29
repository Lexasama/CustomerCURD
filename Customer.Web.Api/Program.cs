using Customer.Domain.Repositories;
using Customer.Domain.Services;
using Customer.Infrastructure;
using Customer.Infrastructure.repositories;
using Customer.Web.Api.Abstractions;
using Customer.Web.Api.Validation;
using Customer.Web.Shared.ActionFilter;
using Customer.Web.Shared.Configurations.HealthChecks;
using Customer.Web.Shared.Configurations.Swagger;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new ArgumentException("The connectionString is null");

builder.Services.AddSingleton(new SqlConnectionFactory(connectionString));
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<ICustomerService, CustomerService>();
builder.Services.AddScoped<ILinkService, Customer.Web.Shared.Services.LinkService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllers(options => options.Filters.Add<AppExceptionFilter>());
builder.Services.AddValidators();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddHealthCheckConfigurations(connectionString);
builder.Services.AddSwaggerConfiguration();


var app = builder.Build();

app.UseHttpsRedirection();
app.UseSwaggerConfiguration();
app.UseHealthCheckConfigurations();

app.MapControllers();
app.Run();

public partial class Program
{
}