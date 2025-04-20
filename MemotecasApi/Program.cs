using System.Data;
using System.Data.Common;
using Application.Interface;
using Application.Profiles;
using Application.Services;
using Infrastructure.Interface;
using Infrastructure.Repository;
using Microsoft.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddAutoMapper(typeof(PensamentosProfile));

string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddScoped<IDbConnection>(provider =>
{
    SqlConnection connection = new SqlConnection(connectionString);
    connection.Open();
    return connection;
});



#region SERVICES
builder.Services.AddScoped<IPensamentoService, PensamentoService>();

#endregion

#region REPOSITORIOS

builder.Services.AddScoped<IPensamentoRepository, PensamentoRepository>();


#endregion

builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", builder =>
    {
        builder.WithOrigins("http://localhost:4200")
               .AllowAnyHeader()
               .AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAngularApp");
app.MapControllers();

app.Run();
