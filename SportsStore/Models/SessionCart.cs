using SportsStore.Infrastructure;
using System.Text.Json.Serialization;

namespace SportsStore.Models;

public class SessionCart : Cart
{
	const string SessionKey = "cart";
	
	[JsonIgnore]
	public ISession? Session { get; set; }
	
	public static Cart GetCart(IServiceProvider services)
	{
		var session = services.GetRequiredService<IHttpContextAccessor>().HttpContext?.Session;
		var cart = session?.GetObject<SessionCart>(SessionKey) ?? new();
		cart.Session = session;
		return cart;
	}

	public override void AddItem(Product product, int quantity)
	{
		base.AddItem(product, quantity);
		Session?.SetObject(SessionKey, this);
	}

	public override void Clear()
	{
		base.Clear();
		Session?.Remove(SessionKey);
	}

	public override void RemoveLine(Product product)
	{
		base.RemoveLine(product);
		Session?.SetObject(SessionKey, this);
	}
}