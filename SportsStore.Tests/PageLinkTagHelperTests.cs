using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Infrastructure;

namespace SportsStore.Tests;
public class PageLinkTagHelperTests
{
	[Fact]
	public void CanGeneratePageLinks()
	{
		// Arrange.

		var urlHelper = new Mock<IUrlHelper>();

		urlHelper
			.SetupSequence(x => x.Action(
				It.IsAny<UrlActionContext>()))
			.Returns("Test/Page1")
			.Returns("Test/Page2")
			.Returns("Test/Page3");

		var urlHelperFactory = new Mock<IUrlHelperFactory>();

		urlHelperFactory
			.Setup(x => x.GetUrlHelper(
				It.IsAny<ActionContext>()))
			.Returns(urlHelper.Object);

		var viewContext = new Mock<ViewContext>();

		var helper = new PageLinkTagHelper(urlHelperFactory.Object)
		{
			PageAction = "Test",
			PageModel = new()
			{
				CurrentPage = 3,
				ItemsPerPage = 10,
				TotalItems = 25
			},
			ViewContext = viewContext.Object
		};

		var context = new TagHelperContext(
			new(), new Dictionary<object, object>(), string.Empty);

		var content = new Mock<TagHelperContent>();

		var output = new TagHelperOutput(
			"div", new(), (_, _) => Task.FromResult(content.Object));

		// Act.

		helper.Process(context, output);

		// Assert.

		Assert.Equal(
			"<a href=\"Test/Page1\">1</a>" +
			"<a href=\"Test/Page2\">2</a>" +
			"<a href=\"Test/Page3\">3</a>",
			output.Content.GetContent());
	}
}