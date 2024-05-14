using byteStream.Employer.Api.Services;
using byteStream.Employer.Api.Utility.ApiFilter;
using byteStream.Employer.Api.Utility.Extension;
using byteStream.Employer.Api.Utility.Mapping;
using byteStream.Employer.API.Data;
using byteStream.Employer.API.Hubs;
using byteStream.Employer.API.Services;
using byteStream.Employer.API.Services.IServices;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.OpenApi.Models;
using System.IO;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(AutoMapperProfile));

builder.AddAppAuthetication();
builder.Services.AddAuthorization();
System.Net.ServicePointManager.ServerCertificateValidationCallback = (senderX, certificate, chain, sslPolicyErrors) => { return true; };

// All the services of Employer Service
builder.Services.AddScoped<IEmployerService, EmployerService>();
builder.Services.AddScoped<IVacancyService, VacancyService>();
builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IProfileService, ProfileService>();
builder.Services.AddScoped<IImageService, ImageService>();


builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<BackendApiAuthenticationHttpClientHandler>();

// configuring MicroServices 
builder.Services.AddHttpClient("Profile", u => u.BaseAddress = new Uri(builder.Configuration["ServiceUrls:jobSeekerAPI"]));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSignalR();
//swagger Authentication
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition(name: JwtBearerDefaults.AuthenticationScheme, securityScheme: new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Description = "Enter the Bearer Authorization string as following: `Bearer Generated-JWT-Token`",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference= new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=JwtBearerDefaults.AuthenticationScheme
                }
            }, new string[]{}
        }
    });
});


builder.Services.AddCors(options =>

{

	options.AddPolicy("CorsPolicy", builder => builder

		.WithOrigins("http://localhost:4200")

		.AllowAnyMethod()

		.AllowAnyHeader()

		.AllowCredentials());

});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
Directory.CreateDirectory(Path.Combine(Directory.GetCurrentDirectory(), "Images"));
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Images")),
    RequestPath = "/Images"
});

app.UseHttpsRedirection();

//CORS Policy
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.MapHub<UpdateEmployerHubs>("/hubs/updateEmployer");
app.UseAuthorization();

app.MapControllers();

app.Run();
