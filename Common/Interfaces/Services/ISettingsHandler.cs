using BonusBot.Common.Models;
using System.Collections.Generic;

namespace BonusBot.Common.Interfaces.Services
{
    public interface ISettingsHandler
    {
        GuildSettingData GetData(string moduleName, string key);
        Dictionary<string, GuildSettingData> GetDatas(string moduleName);
    }
}