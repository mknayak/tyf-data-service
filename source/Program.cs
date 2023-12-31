﻿using System.Reflection;
using Microsoft.EntityFrameworkCore;
using tyf.data.service.DbModels;
using tyf.data.service.Extensions;
using tyf.data.service.Managers;
using tyf.data.service.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<IInstanceRepository, InstanceRepository>();
builder.Services.AddTransient<ISchemaRepository, SchemaRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IConfigurationRepository, ConfigurationRepository>();
builder.Services.AddTransient<ISecurityManager, SecurityManager>();
builder.Services.AddTransient<ICsvManager, CsvManager>();
builder.Services.AddTransient<ICacheManager, CacheManager>();

builder.Services.AddDbContext<TyfDataContext>(option =>
{
    option.UseNpgsql(builder.Configuration.GetConnectionString("dataDbConnection"));
});
builder.Services.AddMemoryCache();

builder.Services.Configure<ErrorMessages>(
    builder.Configuration.GetSection(ErrorMessages.Key));
builder.Services.Configure<SecurityOption>(
    builder.Configuration.GetSection(SecurityOption.Key));
builder.Services.AddSwaggerGen(c =>
    {
        c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory,
            $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
    });
var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    
    app.UseSwagger();
   // app.UseSwaggerUI();
}
app.UseDefaultFiles();
app.UseStaticFiles();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

