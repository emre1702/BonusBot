﻿using BonusBot.WebDashboardModule.Defaults;
using BonusBot.WebDashboardModule.Extensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace BonusBot.WebDashboardModule.Services
{
    public class UserValidationService
    {
        public void AssertIsInGuild(ISession session, string guildId)
        {
            if (!IsInGuild(session, guildId))
                throw new InvalidOperationException("You are not in that guild!");
        }

        public bool IsInGuild(ISession session, string guildId)
        {
            var guilds = session.Get<HashSet<string>>(SessionKeys.UserGuildIds);
            return guilds?.Contains(guildId) == true;
        }
    }
}
