//using MedServiceAPI.Data;
using FluentValidation;
using Mapster;
using MapsterMapper;
using MedService.BL.Services;
using MedService.Contracts.Abstraction.Repositories;
using MedService.Contracts.Abstraction.Services;
using MedService.DAL.Data;
using MedService.DAL.Mappings;
using MedService.DAL.Repositories;
using MedServiceAPI.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddAutoMapper(typeof(Program),typeof(AutoMapperProfile));
var config = new TypeAdapterConfig();
builder.Services.AddSingleton(config);
builder.Services.AddSingleton<IMapper, ServiceMapper>();

var connectionString = builder.Configuration.GetConnectionString("MSSQL");
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(connectionString);
});
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IPatientService, PatientService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IDoctorRepository, DoctorRepository>();
builder.Services.AddScoped<IAdminRepository, AdminRepository>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddScoped<IValidator<AppointmentDateRequestValidator>>();


builder.Services.AddAuthentication().AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!))
    };
});

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
