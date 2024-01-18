global using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.AspNetCore.Authentication.JwtBearer; // NOTE: THIS LINE OF CODE IS NEWLY ADDED
using Microsoft.IdentityModel.Tokens; // NOTE: THIS LINE OF CODE IS NEWLY ADDED
using BlazorAuthenticationTutorial.Server;
using System.Text;
using BlazorAuthenticationTutorial.Server.Data;
using BlazorAuthenticationTutorial.Server.Log;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// NOTE: the following block of code is newly added
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateAudience = false,
        ValidateIssuer = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("MyScretKey34343sfs3434sdValue"))
    };
});
// NOTE: end block

builder.Logging.AddFileLogger("Log/mylog.txt");



builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddScoped<IJwtUtils, JwtUtils>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseAuthentication(); // NOTE: line is newly added
app.UseRouting();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization(); // NOTE: line is newly addded, notice placement after UseRouting()

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
