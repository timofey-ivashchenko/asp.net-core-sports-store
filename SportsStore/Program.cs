using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(
	options => options.UseSqlServer(
		builder.Configuration["ConnectionStrings:Main"]));

builder.Services.AddScoped<IStoreRepository, EfStoreRepository>();

var app = builder.Build();

app.UseStaticFiles();

app.MapControllerRoute(
	"category-page",
	"{category}/page{page:int}",
	new
	{
		controller = "Home",
		action = "Index"
	});

app.MapControllerRoute(
	"page", "page{page:int}",
	new
	{
		controller = "Home",
		action = "Index",
		page = 1
	});

app.MapControllerRoute(
	"category", "{category}",
	new
	{
		controller = "Home",
		action = "Index",
		page = 1
	});

app.MapControllerRoute(
	"products-pagination",
	"products/page{page:int}",
	new
	{
		controller = "Home",
		action = "Index",
		page = 1
	});

app.MapDefaultControllerRoute();

SeedData.EnsurePopulated(app);

app.Run();