using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.AddControllers();

builder.Services.AddAuth(builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>());
builder.Services.AddDb(builder.Configuration.GetSection("ConnectionStrings")["psql"]);
builder.Services.AddHasher();
builder.Services.AddScoped<UsersService>();

var app = builder.Build();

app.Services.SeedData(app.Configuration);

app.UseExceptionHandling();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();