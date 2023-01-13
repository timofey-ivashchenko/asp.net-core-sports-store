using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models;

static class IdentitySeedData
{
	const string AdminUser = "Admin";

	const string AdminPassword = "TopSecret!2023";

	public static async void EnsurePopulated(IApplicationBuilder app)
	{
		var context = app.GetService<AppIdentityDbContext>();

		if ((await context.Database.GetPendingMigrationsAsync()).Any())
			await context.Database.MigrateAsync();

		var userManager = app.GetService<UserManager<IdentityUser>>();
		var user = await userManager.FindByNameAsync(AdminUser);

		if (user is not null) return;

		user = new(AdminUser)
		{
			Email = "tymofii.ivashchenko@gmail.com",
			PhoneNumber = "+380 (99) 725-25-25"
		};

		await userManager.CreateAsync(user, AdminPassword);
	}

	static T GetService<T>(this IApplicationBuilder x) where T : notnull =>
		x.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<T>();
}