using System.Collections.Generic;
using System.Reflection;

namespace BonusBot.Common.Interfaces.Services
{
    public interface IModulesHandler
    {
        List<Assembly> LoadedModuleAssemblies { get; }

        Assembly? FindAssemblyByModuleName(string moduleName, bool includeCommon = true);
    }
}