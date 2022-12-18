using System.Text.Json;

namespace SportsStore.Infrastructure;

public static class SessionExtensions
{
	public static T? GetJson<T>(this ISession session, string key)
	{
		var sessionData = session.GetString(key);

		return sessionData is not null
			? JsonSerializer.Deserialize<T>(sessionData) : default;
	}

	public static void SetJson(
		this ISession session, string key, object value) =>
		session.SetString(key, JsonSerializer.Serialize(value));
}