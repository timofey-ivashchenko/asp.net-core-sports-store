using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SportsStore.Models.ViewModels;

namespace SportsStore.Controllers;

public class AccountController : Controller
{
	readonly SignInManager<IdentityUser> _signInManager;

	readonly UserManager<IdentityUser> _userManager;

	public AccountController(
		UserManager<IdentityUser> userManager,
		SignInManager<IdentityUser> signInManager)
	{
		_userManager = userManager;
		_signInManager = signInManager;
	}

	public ViewResult Login(string returnUrl) =>
		View(new LoginModel { ReturnUrl = returnUrl });

	[HttpPost]
	[ValidateAntiForgeryToken]
	public async Task<IActionResult> Login(LoginModel model)
	{
		if (!ModelState.IsValid) return View(model);
		var user = await _userManager.FindByNameAsync(model.Name!);

		if (user is not null)
		{
			await _signInManager.SignOutAsync();
			var result = await _signInManager.PasswordSignInAsync(
				user, model.Password!, false, false);
			if (result.Succeeded) return Redirect(model.ReturnUrl);
		}

		ModelState.AddModelError("", "Invalid name or password");
		return View(model);
	}

	[Authorize]
	public async Task<RedirectResult> Logout(string returnUrl = "/")
	{
		await _signInManager.SignOutAsync();
		return Redirect(returnUrl);
	}
}