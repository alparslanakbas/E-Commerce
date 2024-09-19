using Autofac.Extensions.DependencyInjection;
using Autofac;
using ETicaretAPI.Persistence;
using ETicaretAPI.Application.Validators.Product;
using FluentValidation;
using ETicaretAPI.Infrastructure.Filters;
using FluentValidation.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

// Autofac konfigurasyonu
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new AutofacModule());
});

// Add services to the container.
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<CreateProductValidator>();

builder.Services.AddControllers(opts => opts.Filters.Add<ValidationFilter>())
                .ConfigureApiBehaviorOptions(opts =>opts.SuppressModelStateInvalidFilter = true)
                ;





// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(opts =>
{
    opts.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200")
              .AllowAnyHeader()
              .AllowAnyMethod()
              ;
    });
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
