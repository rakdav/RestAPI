using Microsoft.EntityFrameworkCore;
using RestAPI.Models;
using RestAPI.Models.Abstractions;
using RestAPI.Models.Data;
using RestAPI.Models.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddDbContext<FirstDbContext>(opt=>opt.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddScoped<ICommonService<Service, int>, ServicesService>();
builder.Services.AddCors();
var app = builder.Build();
app.UseRouting();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
