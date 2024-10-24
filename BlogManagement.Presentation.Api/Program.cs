using _0_Framework.Application;
using _01_LampShade.Query.Contracts.Article;
using _01_LampShade.Query.Query;
using BlogManagement.Infrastructure.Configure;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.Configuration;
using Microsoft.EntityFrameworkCore;
using ServiceHost;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("LampShadeDb");

BlogManagementBootstrapper.Configure(builder.Services, connectionString);
CommentManagementBootstrapper.Configure(builder.Services, connectionString);
builder.Services.AddTransient<IFileUploader, FileUploader>();
//builder.Services.AddTransient<IAuthHelper, AuthHelper>();

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
