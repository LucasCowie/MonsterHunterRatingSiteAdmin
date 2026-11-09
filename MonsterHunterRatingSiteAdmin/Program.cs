using Auth0.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using MonsterHunterRatingSiteAdmin.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("MonsterHunterRatingSiteAdminContext") ?? throw new InvalidOperationException("Connection string 'MonsterHunterRatingSiteAdminContext' not found.");

builder.Services.AddDbContext<MonsterHunterRatingSiteAdminContext>(options => options.UseSqlServer(connectionString));

// Auth0 login; Domain and ClientId come from appsettings / user secrets
builder.Services.AddAuth0WebAppAuthentication(options =>
{
    options.Domain = builder.Configuration["Auth0:Domain"] ?? throw new InvalidOperationException("Auth0:Domain not found.");
    options.ClientId = builder.Configuration["Auth0:ClientId"] ?? throw new InvalidOperationException("Auth0:ClientId not found.");
    // Optional for login only; set it in user secrets to use the authorization code flow
    options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];
});

// Roles arrive in a namespaced ID token claim added by an Auth0 Post-Login Action
builder.Services.Configure<OpenIdConnectOptions>(Auth0Constants.AuthenticationScheme, options =>
{
    options.TokenValidationParameters.NameClaimType = "name";
    options.TokenValidationParameters.RoleClaimType = builder.Configuration["Auth0:RoleClaim"];
});

// Every page requires login unless marked [AllowAnonymous]
builder.Services.AddAuthorization(options =>
{
    options.FallbackPolicy = new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build();
});

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAzureBlobService, AzureBlobService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

// CSS, JS and images must load on the public home page too
app.MapStaticAssets().AllowAnonymous();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
