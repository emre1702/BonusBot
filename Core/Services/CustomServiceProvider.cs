using BonusBot.Common.Interfaces.Core;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BonusBot.Core.Services
{
    public class CustomServiceProvider : ICustomServiceProvider
    {
        private readonly IServiceCollection _serviceCollection;
        private readonly ServiceProvider _serviceProvider;

        public CustomServiceProvider(IServiceCollection collection)
        {
            _serviceCollection = collection;
            collection.AddSingleton<ICustomServiceProvider>(this);
            _serviceProvider = collection.BuildServiceProvider();
        }

        public object GetService(Type serviceType)
            => _serviceProvider.GetService(serviceType);

        public void InitAllSingletons()
        {
            var singletonTypes = GetAllSingletonTypes();
            foreach (var type in singletonTypes)
                _serviceProvider.GetRequiredService(type);
        }

        public IEnumerable<Type> GetAllSingletonTypes()
            => _serviceCollection.Where(s => s.Lifetime == ServiceLifetime.Singleton).Select(s => s.ServiceType);
    }
}