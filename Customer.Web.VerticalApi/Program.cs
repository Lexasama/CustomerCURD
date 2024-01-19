var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblyContaining<Program>());


var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();