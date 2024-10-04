using BaseLibrary.LConnection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Systems.Middleware;
using Systems.Repositories;

string CorsPolicy = "CorsPolicy";

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
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
builder.Services.AddSwaggerGen(options =>
{
    var xmlPath = Path.Combine(AppContext.BaseDirectory, "Systems.xml");
    options.IncludeXmlComments(xmlPath);
    options.SwaggerDoc("V1", new OpenApiInfo
    {
        Version = "V1",
        Title = "WebAPI",
        Description = "IMS WebAPI"
    });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme {
                Reference = new OpenApiReference {
                    Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                }
            },
            new List < string > ()
        }
    });
});
builder.Services.AddCors(o => o.AddPolicy(CorsPolicy, builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = configuration["JWT:ValidIssuer"],
        ValidAudience = configuration["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

var app = builder.Build();

app.UseMiddleware<MConnection>();
// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/V1/swagger.json", "IMS WebAPI");
});
app.UseCors(CorsPolicy);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
