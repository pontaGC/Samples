using Microsoft.Extensions.DependencyInjection;

namespace Services.Core
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

            RegisterIOServices(container);
        }

        private static void RegisterIOServices(IServiceCollection container)
        {
            container.AddSingleton<IFileSystem>(FileSystem.Instance);
        }
    }
}
