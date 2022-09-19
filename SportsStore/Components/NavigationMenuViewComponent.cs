using Microsoft.AspNetCore.Mvc;

namespace SportsStore.Components;

public class NavigationMenuViewComponent : ViewComponent
{
	public string Invoke() => "Hello from the Navigation Menu View Component!";
}