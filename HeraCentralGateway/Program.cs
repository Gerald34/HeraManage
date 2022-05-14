using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Cache.CacheManager;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("Config/OcelotConfiguration.json");

// Add services to the container.
string authenticationProviderKey = "Bearer";

builder.Services.AddAuthentication()
.AddJwtBearer(authenticationProviderKey, options =>
{
    options.Authority = "test";
    options.Audience = "test";
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOcelot()
.AddCacheManager(options => options.WithDictionaryHandle());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseOcelot().Wait();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
