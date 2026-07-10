using System.Text;
using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<DbContext, HerculesContext>(options => options.UseNpgsql(builder.Configuration.GetSection("ConnectionStrings")["psql"]));
builder.Services.AddControllers();
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));

builder.Services.AddRepos();
builder.Services.AddAuth(builder.Configuration.GetSection("JwtOptions").Get<JwtOptions>() ?? throw new NullReferenceException("No Jwt options was found"));

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddSingleton<IJwtProvider, JwtProvider>();
builder.Services.AddScoped<UsersService>();

var app = builder.Build();

app.SeedData();

app.UseExceptionHandling();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();