﻿using System.Text.Json;

namespace WebApplication1.Extensions
{
    public static class ExtensionSession
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.SetString(key, JsonSerializer.Serialize(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.GetString(key);
            return value != null ? JsonSerializer.Deserialize<T>(value) : default;
        }

        public static void Remove(this ISession session, string key)
        {
            session.Remove(key);
        }
    }
}
