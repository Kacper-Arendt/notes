using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using noteApp.MappingProfiles;
using noteApp.Models;

var builder = WebApplication.CreateBuilder(args);

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appSettings.json")
    .Build();

builder.Services.AddControllers();

builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseSqlServer(config.GetConnectionString("DefaultConnection"));
});

//builder.Services.AddCors((options) =>
//{
//    options.AddPolicy("DevCors", (corsBuilder) =>
//    {
//        corsBuilder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
//            .AllowAnyMethod()
//            .AllowAnyHeader()
//            .AllowCredentials();
//    });
//    options.AddPolicy("ProdCors", (corsBuilder) =>
//    {
//        corsBuilder.WithOrigins("https://notes-web-rust.vercel.app")
//            .AllowAnyMethod()
//            .AllowAnyHeader()
//            .AllowCredentials();
//    });
//});

string? tokenKey = builder.Configuration.GetSection("AppSettings:TokenKey").Value;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters() 
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenKey ?? "")),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseCors("DevCors");
    app.UseSwaggerUI();
}
else
{
    app.UseHttpsRedirection();
    //app.UseCors("ProdCors");
}



using (var scope = app.Services.CreateScope())
{
var services = scope.ServiceProvider;

var context = services.GetRequiredService<DatabaseContext>();
context.Database.EnsureCreated();
}


app.UseDeveloperExceptionPage();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
