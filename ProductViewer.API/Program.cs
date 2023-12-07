using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using ProductViewer.DataAccess;
using ProductViewer.DataAccess.Repositories;
using ProductViewer.Database.Entities;
using ProductViewer.EntityFramework;
using ProductViewer.Services.ProductService;
using System.Data;

var builder = WebApplication.CreateBuilder(args);
var services = builder.Services;

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

string defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection");
services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(defaultConnection));

SqlConnection sqlConnection = new SqlConnection(defaultConnection);
services.AddSingleton<IDbConnection>(sqlConnection);

services.AddScoped<IGenericRepository<ProductEntity>, ProductRepository>();

services.AddTransient<IProductService, ProductService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
