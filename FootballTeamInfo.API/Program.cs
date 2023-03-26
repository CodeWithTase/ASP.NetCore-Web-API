using FootballTeamInfo.API;
using FootballTeamInfo.API.DbContexts;
using FootballTeamInfo.API.Services;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;

Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.Console()
    .WriteTo.File("logs/footballteamsinfo.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();
var builder = WebApplication.CreateBuilder(args);

//use serilog for logging
builder.Host.UseSerilog();
// Add services to the container.
//set to return not supported when consumer asks for certiain represaentation ps now configured to return xml
builder.Services.AddControllers( options => { options.ReturnHttpNotAcceptable = true; })
    .AddNewtonsoftJson()
    .AddXmlDataContractSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FileExtensionContentTypeProvider>();

//Add auotmapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//configure bearer token
builder.Services.AddAuthentication("Bearer").AddJwtBearer(
    options => 
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    });

//configure authorization policy
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("MustSupportArsenal", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("footballTeam", "Arsenal");
    });
});

//Add database context
builder.Services.AddDbContext<FootbalTeamInfoContext>(
    dbContextOptionx => dbContextOptionx.UseSqlite(
        builder.Configuration["ConnectionStrings:FootballTeamInfoConnectionStrings"]));

//add mail service
#if DEBUG
builder.Services.AddTransient<IMailService, LocalMailService>();
#else
builder.Services.AddTransient<IMailService, CloudMailService>();
#endif

//add datastore
builder.Services.AddSingleton(new FootballTeamDataStore());

//add repository
builder.Services.AddScoped<IFootballTeamInfoRepository, FootballTeamInfoRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
