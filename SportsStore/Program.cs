using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(
	options => options.UseSqlServer(
		builder.Configuration["ConnectionStrings:Main"]!));

builder.Services.AddScoped<IStoreRepository, EfStoreRepository>();

builder.Services.AddRouting(options => options.LowercaseUrls = true);

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
	"category", "c/{category}",
	new
	{
		controller = "Home",
		action = "Index",
		page = 1
	});

app.MapControllerRoute(
	"category-page",
	"c/{category}/p{page:int}",
	new
	{
		controller = "Home",
		action = "Index",
		page = 1
	});

app.MapControllerRoute(
	"page", "p{page:int}",
	new
	{
		controller = "Home",
		action = "Index",
		page = 1
	});

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();