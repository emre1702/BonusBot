using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.Common.Interfaces.Services
{
    public interface IModulesHandler
    {
        List<Assembly> LoadedModuleAssemblies { get; }
        TaskCompletionSource LoadModulesTaskSource { get; }

        Assembly? FindAssemblyByModuleName(string moduleName, bool includeCommon = true);
    }
}