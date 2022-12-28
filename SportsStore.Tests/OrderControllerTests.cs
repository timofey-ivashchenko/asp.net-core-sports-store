using Microsoft.AspNetCore.Mvc;
using SportsStore.Controllers;
using SportsStore.Models;

namespace SportsStore.Tests;

public class OrderControllerTests
{
	[Fact]
	public void CanCheckoutAndSubmitOrder()
	{
		// Arrange.

		// Создаём фиктивный репозиторий заказов.
		var repository = new Mock<IOrderRepository>();

		// Создаём корзину с одним элементом.
		var cart = new Cart();
		cart.AddItem(new(), 1);

		// Создаём контроллер заказов.
		var controller = new OrderController(repository.Object, cart);

		// Создаём заказ с известным идентификатором.
		var order = new Order { OrderID = 13 };

		// Act.

		// Оформляем заказ.
		var result = controller.Checkout(order) as RedirectToPageResult;

		// Assert.

		// Проверяем, что заказ сохраняется один раз.
		repository.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Once);
		// Проверяем, что пользователь будет перенаправлен.
		Assert.NotNull(result);
		// Проверяем страницу, на которую будет перенаправлен пользователь.
		Assert.Equal("/completed", result?.PageName);
		// Проверяем количество параметров в строке запроса.
		Assert.Equal(1, result?.RouteValues?.Count);
		// Проверяем наличие параметра orderId.
		Assert.True(result?.RouteValues?.ContainsKey("orderId"));
		// Проверяем значение параметра orderId.
		Assert.Equal(order.OrderID, result?.RouteValues?["orderId"]);
	}

	[Fact]
	public void CannotCheckoutEmptyCart()
	{
		// Arrange.

		// Создаём фиктивный репозиторий заказов.
		var repository = new Mock<IOrderRepository>();

		// Создаём пустую корзину.
		var cart = new Cart();

		// Создаём контроллер заказов.
		var controller = new OrderController(repository.Object, cart);

		// Act.

		var result = controller.Checkout(new()) as ViewResult;

		// Assert.

		// Проверяем, что заказ не был сохранён.
		repository.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Never);

		// Проверяем, что метод возвращает представление по умолчанию.
		Assert.True(string.IsNullOrEmpty(result?.ViewName));

		// Проверяем, что в представление передаётся недействительная модель.
		Assert.False(result?.ViewData.ModelState.IsValid);
	}

	[Fact]
	public void CannotCheckoutInvalidShippingDetails()
	{
		// Arrange.

		// Создаём фиктивный репозиторий заказов.
		var repository = new Mock<IOrderRepository>();

		// Создаём корзину с одним элементом.
		var cart = new Cart();
		cart.AddItem(new(), 1);

		// Создаём контроллер заказов.
		var controller = new OrderController(repository.Object, cart);

		// Добавляем ошибку модели.
		controller.ModelState.AddModelError("error", "error");

		// Act.

		var result = controller.Checkout(new()) as ViewResult;

		// Assert.

		// Проверяем, что заказ не был сохранён.
		repository.Verify(x => x.SaveOrder(It.IsAny<Order>()), Times.Never);

		// Проверяем, что метод возвращает представление по умолчанию.
		Assert.True(string.IsNullOrEmpty(result?.ViewName));

		// Проверяем, что в представление передаётся недействительная модель.
		Assert.False(result?.ViewData.ModelState.IsValid);
	}
}