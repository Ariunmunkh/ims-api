using BaseLibrary.LConnection;
using Microsoft.AspNetCore.Cors.Infrastructure;
using System.Data.Common;
using Systems.Middleware;
using Systems.Repositories;

string CorsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddTransient<ISystemsRepository, SystemsRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBaseRepository, BaseRepository>();
builder.Services.AddTransient<IVolunteerRepository, VolunteerRepository>();
builder.Services.AddTransient<ICommitteeRepository, CommitteeRepository>();
builder.Services.AddScoped(service => new DWConnector());

builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(o => o.AddPolicy(CorsPolicy, builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var app = builder.Build();

app.UseMiddleware<MConnection>();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();
app.UseCors(CorsPolicy);

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
