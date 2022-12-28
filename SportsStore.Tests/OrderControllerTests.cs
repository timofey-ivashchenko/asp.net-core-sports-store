using Microsoft.AspNetCore.Mvc;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests;

public class OrderControllerTests
{
	[Fact]
	public void CannotCheckoutEmptyCart()
	{
		// Arrange.

		var repository = new Mock<IOrderRepository>();
		var cart = new Cart();
		var controller = new OrderController(repository.Object, cart);
		var order = new Order();

		// Act.

		var result = controller.Checkout(order) as ViewResult;

		// Assert.

		// Проверяем, что заказ не был сохранён.
		repository.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Never);

		// Проверяем, что метод возвращает представление по умолчанию.
		Assert.True(string.IsNullOrEmpty(result?.ViewName));

		// Проверяем, что в представление передаётся недействительная модель.
		Assert.False(result?.ViewData.ModelState.IsValid);
	}
}