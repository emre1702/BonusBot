using BonusBot.Common.Defaults;
using System.Globalization;

namespace BonusBot.Services.System
{
    public class GlobalizationHandler
    {
        public GlobalizationHandler()
        {
            CultureInfo.DefaultThreadCurrentCulture = Constants.Culture;
            CultureInfo.DefaultThreadCurrentUICulture = Constants.Culture;
        }
    }
}