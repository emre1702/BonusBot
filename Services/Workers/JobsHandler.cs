using BonusBot.Common;
using BonusBot.Common.Enums;
using BonusBot.Common.Helper;
using BonusBot.Common.Interfaces.Services;
using BonusBot.Common.Interfaces.Workers;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BonusBot.Services.Workers
{
    internal class JobsHandler
    {
        public JobsHandler(IServiceProvider serviceProvider, IModulesHandler modulesHandler)
        {
            StartAllJobs(serviceProvider, modulesHandler);
        }

        private async void StartAllJobs(IServiceProvider serviceProvider, IModulesHandler modulesHandler)
        {
            try
            {
                await modulesHandler.LoadModulesTaskSource.Task;
                // Delay to make sure everythings loaded
                await Task.Delay(5000);

                var jobTypes = GetAllJobTypes(modulesHandler);
                var jobs = CreateJobs(serviceProvider, jobTypes);

                foreach (var job in jobs)
                    job.Start();
            }
            catch (Exception ex)
            {
                ConsoleHelper.Log(LogSeverity.Error, LogSource.Core, "Loading all jobs failed.", ex);
            }
        }

        private IEnumerable<Type> GetAllJobTypes(IModulesHandler modulesHandler)
        {
            var jobType = typeof(IJob);
            return modulesHandler.LoadedModuleAssemblies
                .Union(new List<Assembly> { typeof(CommonSettings).Assembly })
                .SelectMany(a => a.GetTypes())
                .Where(t => t.IsAssignableTo(jobType) && !t.IsAbstract);
        }

        private IEnumerable<IJob> CreateJobs(IServiceProvider serviceProvider, IEnumerable<Type> jobTypes)
        {
            foreach (var jobType in jobTypes)
                yield return (IJob)ActivatorUtilities.CreateInstance(serviceProvider, jobType);
        }
    }
}