using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using System.Globalization;

var culture = new CultureInfo("en-US");
CultureInfo.CurrentCulture = culture;
CultureInfo.CurrentUICulture = culture;
CultureInfo.DefaultThreadCurrentCulture = culture;
CultureInfo.DefaultThreadCurrentUICulture = culture;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(
	options => options.UseSqlServer(
		builder.Configuration["ConnectionStrings:SportsStoreConnection"]!));

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

builder.Services.AddRazorPages();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession();

builder.Services.AddScoped(SessionCart.GetCart);
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

app.UseStaticFiles();
app.UseSession();

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

SeedData.EnsurePopulated(app);

app.Run();