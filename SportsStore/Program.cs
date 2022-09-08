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

app.MapDefaultControllerRoute();

app.MapControllerRoute(
	"products-pagination",
	"products/page{page}",
	new
	{
		Controller = "Home",
		Action = "Index"
	});

SeedData.EnsurePopulated(app);

app.Run();