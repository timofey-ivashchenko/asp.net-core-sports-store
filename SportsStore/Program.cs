using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(
	options => options.UseSqlServer(
		builder.Configuration["ConnectionStrings:Main"]!));

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();
builder.Services.AddScoped<IOrderRepository, EFOrderRepository>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped(SessionCart.GetCart);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddServerSideBlazor();

builder.Services.AddDbContext<AppIdentityDbContext>(
	options => options.UseSqlServer(
		builder.Configuration["ConnectionStrings:Identity"]));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
	.AddEntityFrameworkStores<AppIdentityDbContext>();

var app = builder.Build();

if (app.Environment.IsProduction())
	app.UseExceptionHandler("/error");

app.UseRequestLocalization(x => x
	.AddSupportedCultures("en-US")
	.AddSupportedUICultures("en-US")
	.SetDefaultCulture("en-US"));

app.UseStaticFiles();
app.UseSession();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute("website-home", "",
	new { controller = "Home", action = "Index", category = "", productPage = 1 });

app.MapControllerRoute("category", "c/{category}",
	new { controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute("category-page", "c/{category}/p{productPage:int}",
	new { controller = "Home", action = "Index" });

app.MapControllerRoute("page", "p{productPage:int}",
	new { controller = "Home", action = "Index", category = "" });

app.MapDefaultControllerRoute();
app.MapRazorPages();

app.MapBlazorHub();
app.MapFallbackToPage("/admin/{*catchall}", "/Admin/Index");

SeedData.EnsurePopulated(app);
IdentitySeedData.EnsurePopulated(app);

app.Run();