using HeraManage.Config;
using HeraManage.Services;
using HeraManage.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// add services to DI container
{
    var services = builder.Services;
    services.AddCors();

    // configure strongly typed settings object
    services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

    // configure DI for application services
    services.AddScoped<IUserRepository, UserService>();
    services.AddScoped<UserService, UserService>();
    services.AddScoped<ClientService, ClientService>();
    services.AddScoped<ClientPointsService, ClientPointsService>();

    // services.AddSingleton<IUserRepository, IUserRepository>();
}

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Standard Authorisation header using the Bearer scheme (\"bearer {token}\")",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value)),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

// DbContext
builder.Services.AddDbContext<UsersDbContext>(
        options => options.UseSqlServer(
            builder.Configuration.GetConnectionString("UsersDbConnectionString")
        )
    );
builder.Services.AddDbContext<ClientDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("ClientsDbConnectionString")
    )
);
builder.Services.AddDbContext<SystemDbContext>(
    options => options.UseSqlServer(
        builder.Configuration.GetConnectionString("SystemDbConnectionString")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
// configure HTTP request pipeline
{
    // global cors policy
    app.UseCors(x => x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader());

    app.MapControllers();
}
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

