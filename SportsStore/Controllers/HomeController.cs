﻿using Microsoft.AspNetCore.Mvc;
using SportsStore.Models;

namespace SportsStore.Controllers;

public class HomeController : Controller
{
	readonly IStoreRepository _repository;

	public HomeController(IStoreRepository repository) =>
		_repository = repository;

	public IActionResult Index() => View(_repository.Products);
}