using System.IdentityModel.Tokens.Jwt;
using BlogLab.Identity;
using BlogLab.Models.Account;
using BlogLab.Models.Settings;
using BlogLab.Web;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BlogLab.Web.Extensions;

var builder = WebApplication.CreateBuilder(args);

JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.Configure<CloudinaryOptions>(builder.Configuration.GetSection("CloudinaryOptions"));

builder.Services.AddBlogServices();
builder.Services.AddRepositories();

builder.Services.AddIdentityCore<ApplicationUserIdentity>(opts =>
        opts.Password.RequireNonAlphanumeric = false)
    .AddUserStore<UserStore>()
    .AddDefaultTokenProviders()
    .AddSignInManager<SignInManager<ApplicationUserIdentity>>();

builder.Services.AddControllers();
builder.Services.AddCors();

builder.Services.AddAuthentication(opts =>
{
    opts.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    opts.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opts =>
{
    opts.RequireHttpsMetadata = false;
    opts.SaveToken = true;
    opts.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey =
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"] ?? string.Empty)),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.ConfigureExceptionHandler();

app.UseRouting();

if (app.Environment.IsDevelopment())
{
    app.UseCors(opts => opts.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
}
else
{
    app.UseCors();
}

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
