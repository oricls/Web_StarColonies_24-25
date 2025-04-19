using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StarColonies.Domains;
using StarColonies.Infrastructures;
using StarColonies.Infrastructures.Entities;
using StarColonies.Web.Constraints;
using StarColonies.Web.Middlewares;
using Colon = StarColonies.Infrastructures.Entities.Colon;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddScoped<ReverseProxyLinksMiddleware>();
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
    options.ConstraintMap.Add("isSlug", typeof(SlugConstraint));
});


builder.Services
.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
.AddCookie();

builder.Services.AddDbContext<StarColoniesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection") 
                         ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

builder.Services.AddIdentity<Colon,IdentityRole>()
    .AddEntityFrameworkStores<StarColoniesContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IColonRepository, EfColonRepository>();
builder.Services.AddScoped<IMissionRepository, EfMissionRepository>();
builder.Services.AddScoped<ITeamRepository, EfTeamRepository>();
builder.Services.AddScoped<IBonusRepository, EfBonusRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions());
app.UseReverseProxyLinks();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();