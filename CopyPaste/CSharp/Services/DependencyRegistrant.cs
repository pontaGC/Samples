﻿using Microsoft.Extensions.DependencyInjection;
using Services.Serialization;
using Services.Core;
using Services.Core.Serialization;

namespace Services
{
    /// <inheritdoc />
    public class DependencyRegistrant : IDependenyRegistrant
    {
        /// <inheritdoc />
        public void Register(IServiceCollection container)
        {
            if (container is null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            RegisterSerializationServices(container);
        }

        private static void RegisterSerializationServices(IServiceCollection container)
        {
            container.AddSingleton<IXmlSerializer, XmlSerializerImpl>();
        }
    }
}
