using System.Text.Json;

namespace SportsStore.Infrastructure;

public static class SessionExtensions
{
	public static T? GetObject<T>(this ISession session, string key)
	{
		var objectJson = session.GetString(key);

		return objectJson is not null
			? JsonSerializer.Deserialize<T>(objectJson) : default;
	}

	public static void SetObject(
		this ISession session, string key, object value) =>
		session.SetString(key, JsonSerializer.Serialize(value));
}