﻿using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace BonusBot.WebDashboardModule.Extensions
{
    public static class SessionExtensions
    {
        public static void Set<T>(this ISession session, string key, T value)
        {
            session.Set(key, JsonSerializer.SerializeToUtf8Bytes(value));
        }

        public static T? Get<T>(this ISession session, string key)
        {
            var value = session.Get(key);

            return value == null ? default :
                JsonSerializer.Deserialize<T>(value);
        }

        public static bool Has(this ISession session, string key)
            => session.Get(key) is not null;
    }
}
