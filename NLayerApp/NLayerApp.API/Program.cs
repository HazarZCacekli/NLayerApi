using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NLayerApp.API.Filters;
using NLayerApp.API.Modules;
using NLayerApp.Repository.DBContext;
using NLayerApp.Service.Mapping;
using NLayerApp.Service.Validations;
using System.Reflection;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add(new ValidateFilterAttribute())).AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<ProductDTOValidator>()).AddJsonOptions(x=>
x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(MapProfile));

builder.Services.AddDbContext<AppDBContext>(x =>
{
    x.UseNpgsql(builder.Configuration.GetConnectionString("SqlCon"), options =>
    {
        options.MigrationsAssembly(Assembly.GetAssembly(typeof(AppDBContext)).GetName().Name);
    });
});

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
    containerBuilder.RegisterModule(new RepoServiceModule());
});

builder.Services.AddMemoryCache();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
