using SportsStore.Infrastructure;

namespace SportsStore.Models;

public class SessionCart : Cart
{
	const string SessionKey = "cart";

	ISession? _session;
	
	public static Cart GetCart(IServiceProvider services)
	{
		var session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
		var cart = session?.GetObject<SessionCart>(SessionKey) ?? new();
		cart._session = session;
		return cart;
	}

	public override void AddItem(Product product, int quantity)
	{
		base.AddItem(product, quantity);
		_session?.SetObject(SessionKey, this);
	}

	public override void Clear()
	{
		base.Clear();
		_session?.Remove(SessionKey);
	}

	public override void RemoveLine(Product product)
	{
		base.RemoveLine(product);
		_session?.SetObject(SessionKey, this);
	}
}