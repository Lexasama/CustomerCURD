using Customer.Domain.Repositories;
using Customer.Domain.Services;
using Customer.Infrastructure.repositories;
using Customer.Web.Api.Abstractions;
using Customer.Web.Api.Validation;
using Customer.Web.MinimalApi.Endpoints;
using Customer.Web.Shared.ActionFilter;
using Customer.Web.Shared.Configurations.HealthChecks;
using Customer.Web.Shared.Configurations.Swagger;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new ArgumentException("The connectionString is null");

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


app.MapCustomerEndpoints();
app.UseSwaggerConfiguration();
app.UseHealthCheckConfigurations();

app.Run();

//Todo : use the repository or service in endpoint 