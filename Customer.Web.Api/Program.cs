using Customer.Domain.Repositories;
using Customer.Domain.Services;
using Customer.Infrastructure;
using Customer.Infrastructure.repositories;
using Customer.Web.Api.Abstractions;
using Customer.Web.Api.ActionFilter;
using Customer.Web.Api.Configurations;
using Customer.Web.Api.Feature.Customers;
using Customer.Web.Api.Services;
using Customer.Web.Api.Validation;
using MediatR;

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

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());

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


app.MapCustomerEndpointsV2();

// app.MapGet("/api/v2/customers{id:int}", async (int id, ISender sender) =>
// {
//     var query = new GetCustomerQuery(id);
//     var result = await sender.Send(query);
//     return Results.Ok(result);
// }).WithName("GetCustomerV2");
//

app.MapControllers();
app.Run();

public partial class Program
{
}